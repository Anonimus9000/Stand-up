using System;
using System.Collections.Generic;
using System.Linq;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.ProjectLibraries.UISystem.Managers.UiServiceProvider
{
public class PopupsUIService : IUIService
{
    public IUIViewModel CurrentUI { get; private set; }
    public event Action ViewShown;
    public event Action ViewClosed;
    public event Action ViewHidden;

    private readonly List<IUIView> _popupPrefabs;
    private readonly IInstantiater _instantiater;
    private readonly List<IUIViewModel> _queueHiddenPopups = new(10);
    private readonly IAnimatorService _animatorService;

    public PopupsUIService(
        IInstantiater instantiater,
        List<IUIView> mainUIPrefabs, 
        IAnimatorService animatorService)
    {
        _instantiater = instantiater;
        _animatorService = animatorService;
        _popupPrefabs = mainUIPrefabs;
    }

    public void Show<T>(IUIViewModel viewModel) where T : IUIView
    {
        if (CurrentUI == viewModel)
        {
            return;
        }
        
        HideCurrentPopupIfNeed();

        var view = Create<T>();

        viewModel.ViewShown += OnViewShown;
        viewModel.Init(view, _animatorService);
        viewModel.ShowView();
    }

    public void CloseCurrentView()
    {
        if (CurrentUI != null)
        {
            CurrentUI.ViewHidden += OnViewClosed;
            CurrentUI.HideView();
        }
    }

    public void CloseAll()
    {
        foreach (var queueHiddenPopup in _queueHiddenPopups)
        {
            Destroy(queueHiddenPopup);
        }

        _queueHiddenPopups.Clear();

        if (CurrentUI != null)
        {
            CurrentUI.ViewHidden += OnViewClosed;
            CurrentUI?.HideView();
        }
    }

    private void HideCurrentPopupIfNeed()
    {
        if (CurrentUI == null) return;

        CurrentUI.ViewHidden += OnViewHidden;
        CurrentUI.HideView();
    }

    private void OnViewShown(IUIViewModel viewModel)
    {
        viewModel.ViewShown -= OnViewShown;
        
        _queueHiddenPopups.Remove(viewModel);
        CurrentUI = viewModel;
        
        ViewShown?.Invoke();
    }

    private void OnViewHidden(IUIViewModel viewModel)
    {
        viewModel.ViewHidden -= OnViewHidden;
        _queueHiddenPopups.Add(viewModel);
        viewModel.Deinit();
        
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
        CurrentUI = Destroy(CurrentUI);
        viewModel.Deinit();

        _queueHiddenPopups.Remove(CurrentUI);

        if (viewModel == CurrentUI)
        {
            CurrentUI = null;
        }

        TryShowLastHiddenPopup();
        
        ViewClosed?.Invoke();
    }

    private void TryShowLastHiddenPopup()
    {
        if (_queueHiddenPopups.Count <= 0) return;

        var viewModel = _queueHiddenPopups.Last();

        Activate(viewModel);
        viewModel.ViewShown += OnViewShown;
        viewModel.ShowHiddenView();
    }

    private T Create<T>() where T : IUIView
    {
        var uiView = GetPrefab<T>();
        var instantiate = _instantiater.Instantiate(uiView);

        return (T)instantiate;
    }

    private void Activate(IUIViewModel viewModel)
    {
        var instantiatable = viewModel.GetInstantiatable();
        _instantiater.SetActive(instantiatable, true);
    }

    private void Deactivate(IUIViewModel viewModel)
    {
        var popupDialog = viewModel.GetInstantiatable();
        _instantiater.SetActive(popupDialog, false);
    }

    private IUIViewModel Destroy(IUIViewModel viewModel)
    {
        if (viewModel == null)
        {
            return null;
        }

        var instantiatable = viewModel.GetInstantiatable();
        _instantiater.Destroy(instantiatable);

        viewModel = null;

        return null;
    }

    private IUIView GetPrefab<T>() where T : IUIView
    {
        foreach (var popupPrefab in _popupPrefabs)
        {
            if (popupPrefab is T view)
            {
                return view;
            }
        }

        throw new Exception($"Can't find prefab {typeof(T)}");
    }
}
}