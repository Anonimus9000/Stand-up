using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UiServiceProvider
{
public class FullScreensUIService : IUIService
{
    public IUIViewModel CurrentUI { get; private set; }
    
    public event Action ViewShown;
    public event Action ViewClosed;
    public event Action ViewHidden;
    
    private readonly List<IUIView> _fullScreenDialogPrefabs;
    private readonly IInstantiater _instantiater;
    private readonly List<IUIViewModel> _queueHiddenScreens = new();
    private readonly IUIService _popupsUIService;
    private readonly IAnimatorService _animatorService;

    public FullScreensUIService(
        IInstantiater instantiater,
        List<IUIView> mainUIPrefabs,
        IUIService popupsUIService, 
        IAnimatorService animatorService)
    {
        _popupsUIService = popupsUIService;
        _animatorService = animatorService;
        _instantiater = instantiater;
        _fullScreenDialogPrefabs = mainUIPrefabs;
    }

    public void Show<T>(IUIViewModel viewModel) where T : IUIView
    {
        if (CurrentUI == viewModel)
        {
            return;
        }
        
        _popupsUIService.CloseAll();
        
        HideCurrentScreenIfNeed();

        var view = Create<T>();

        viewModel.ViewShown += OnViewShown;
        viewModel.Init(view, _animatorService);
        viewModel.ShowView();
    }

    public void CloseCurrentView()
    {
        if (CurrentUI == null) return;

        CurrentUI.ViewHidden += OnViewClosed;
        CurrentUI.HideView();
    }

    private void Destroy(IUIViewModel viewModel)
    {
        if (viewModel == null)
        {
            return;
        }
        
        var instantiatable = viewModel.GetInstantiatable();
        _instantiater.Destroy(instantiatable);
    }

    private void HideCurrentScreenIfNeed()
    {
        if (CurrentUI == null) return;

        CurrentUI.ViewHidden += OnViewHidden;
        CurrentUI.HideView();
    }

    public void CloseAll()
    {
        foreach (var queueHiddenScreen in _queueHiddenScreens)
        {
            Destroy(queueHiddenScreen);
        }
        
        _queueHiddenScreens.Clear();

        if (CurrentUI != null)
        {
            CurrentUI.ViewHidden += OnViewClosed;
            CurrentUI.HideView();
        }
    }

    private void TryShowLastHiddenScreen()
    {
        if (_queueHiddenScreens.Count <= 0) return;
        
        var viewModel = _queueHiddenScreens[^1];

        Activate(viewModel);
        
        viewModel.ViewShown += OnViewShown;
        viewModel.ShowHiddenView();
    }

    private void OnViewShown(IUIViewModel viewModel)
    {
        viewModel.ViewShown -= OnViewShown;

        CurrentUI = viewModel;
        
        _queueHiddenScreens.Remove(viewModel);
        
        ViewShown?.Invoke();
    }

    private void OnViewHidden(IUIViewModel viewModel)
    {
        viewModel.ViewHidden -= OnViewHidden;
        viewModel.Deinit();
        
        _queueHiddenScreens.Add(viewModel);
        
        Deactivate(viewModel);

        if (CurrentUI == viewModel)
        {
            CurrentUI = null;
        }
        
        ViewHidden?.Invoke();
    }

    private void OnViewClosed(IUIViewModel viewModel)
    {
        viewModel.ViewHidden -= OnViewClosed;
        viewModel.Deinit();
        
        _queueHiddenScreens.Remove(viewModel);
        
        Destroy(viewModel);
        
        if (CurrentUI == viewModel)
        {
            CurrentUI = null;
        }
        
        ViewClosed?.Invoke();

        TryShowLastHiddenScreen();
    }

    private void Activate(IUIViewModel viewModel)
    {
        var instantiatable = viewModel.GetInstantiatable();
        _instantiater.SetActive(instantiatable, true);
    }

    private void Deactivate(IUIViewModel viewModel)
    {
        var instantiatable = viewModel.GetInstantiatable();
        _instantiater.SetActive(instantiatable, false);
    }

    private T Create<T>() where T : IUIView
    {
        var screenToShow = GetPrefab<T>();

        var uiView = _instantiater.Instantiate(screenToShow) as IUIView;

        return (T)uiView;
    }

    private T GetPrefab<T>() where T : IUIView
    {
        foreach (var dialogPrefab in _fullScreenDialogPrefabs)
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