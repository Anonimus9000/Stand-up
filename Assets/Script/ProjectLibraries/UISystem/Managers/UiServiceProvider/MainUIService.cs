using System;
using System.Collections.Generic;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.ProjectLibraries.UISystem.Managers.UiServiceProvider
{
public class MainUIService : IUIService
{
    public IUIViewModel CurrentUI { get; private set; }
    
    public event Action ViewShown;
    public event Action ViewClosed;
    public event Action ViewHidden;
    
    private readonly IUIService _popupsService;
    private readonly IUIService _fullScreensService;
    
    private readonly IInstantiater _instantiater;
    private readonly List<IUIView> _mainUIPrefabs;
    private readonly IAnimatorService _animatorService;

    public MainUIService(
        IInstantiater instantiater, 
        List<IUIView> mainUIPrefabs, 
        IUIService popupsService, 
        IUIService fullScreensService, 
        IAnimatorService animatorService)
    {
        _popupsService = popupsService;
        _fullScreensService = fullScreensService;
        _animatorService = animatorService;
        
        _instantiater = instantiater;
        _mainUIPrefabs = mainUIPrefabs;
    }

    public void Show<T>(IUIViewModel viewModel) where T : IUIView
    {
        if (CurrentUI == viewModel)
        {
            return;
        }
        
        _popupsService.CloseAll();
        _fullScreensService.CloseAll();

        CloseCurrentIfNeed();

        var view = Create<T>();

        viewModel.ViewShown += OnViewShown;
        viewModel.Init(view, _animatorService);
        viewModel.ShowView();
    }

    public void CloseCurrentView()
    {
        CurrentUI.ViewHidden += OnViewClosed;
        CurrentUI.HideView();
    }

    public void CloseAll()
    {
        CloseCurrentIfNeed();
    }

    public void CloseCurrentIfNeed()
    {
        if (CurrentUI == null) return;
        
        CurrentUI.ViewHidden += OnViewClosed;
        CurrentUI.HideView();
    }

    private void OnViewShown(IUIViewModel viewModel)
    {
        viewModel.ViewShown -= OnViewShown;

        CurrentUI = viewModel;
        
        ViewShown?.Invoke();
    }

    private void OnViewClosed(IUIViewModel viewModel)
    {
        CurrentUI.ViewHidden -= OnViewClosed;
        viewModel.Deinit();

        CurrentUI = Destroy(CurrentUI);
        
        if (CurrentUI == viewModel)
        {
            CurrentUI = null;
        }
        
        ViewHidden?.Invoke();
        ViewClosed?.Invoke();
    }

    private IUIViewModel Destroy(IUIViewModel viewModel)
    {
        if (viewModel == null)
        {
            return null;
        }

        var instantiatable = viewModel.GetInstantiatable();
        _instantiater.Destroy(instantiatable);

        return null;
    }

    private T Create<T>() where T : IUIView
    {
        var mainUIToShow = GetPrefab<T>();

        var uiView = _instantiater.Instantiate(mainUIToShow) as IUIView;

        return (T)uiView;
    }

    private IInstantiatable GetPrefab<T>() where T : IUIView
    {
        foreach (var dialogPrefab in _mainUIPrefabs)
        {
            if (dialogPrefab is T view)
            {
                return view;
            }
        }

        throw new Exception($"Can't find prefab {typeof(T)}");
    }
}
}