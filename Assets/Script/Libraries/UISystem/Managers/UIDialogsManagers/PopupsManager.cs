using System;
using System.Collections.Generic;
using System.Linq;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public class PopupsManager : IDialogsManager
{
    private readonly List<IUIView> _popupPrefabs;
    private readonly IInstantiater _instantiater;
    private UIViewModel _currentViewModel;
    private readonly List<UIViewModel> _queueHiddenPopups = new();
    private readonly IUISystem _iuiSystem;

    public PopupsManager(IInstantiater instantiater, List<IUIView> mainUIPrefabs, IUISystem iuiSystem)
    {
        _iuiSystem = iuiSystem;

        _instantiater = instantiater;

        _popupPrefabs = mainUIPrefabs;
    }

    public T Show<T>(UIViewModel viewModel) where T : IUIView
    {
        TryHideCurrentPopup();

        var popupDialog = Create<T>();

        _currentViewModel = viewModel;

        popupDialog!.SetUiManager(_iuiSystem);
        popupDialog!.OnShown();

        _queueHiddenPopups.Add(viewModel);

        return popupDialog;
    }

    private bool TryHideCurrentPopup()
    {
        if (_currentViewModel is null)
        {
            return false;
        }

        Deactivate(_currentViewModel);

        return true;
    }

    public bool TryCloseCurrent()
    {
        if (_currentViewModel == null)
        {
            return false;
        }

        _currentViewModel = Destroy(_currentViewModel);

        _queueHiddenPopups.Remove(_currentViewModel);

        _currentViewModel = null;

        TryShowLastHiddenPopup();

        return true;
    }

    public void CloseAll()
    {
        foreach (var queueHiddenPopup in _queueHiddenPopups)
        {
            Destroy(queueHiddenPopup);
        }

        _queueHiddenPopups.Clear();

        _currentViewModel = Destroy(_currentViewModel);
    }

    private bool TryShowLastHiddenPopup()
    {
        if (_queueHiddenPopups.Count > 0)
        {
            var viewModel = _queueHiddenPopups.Last();
            _queueHiddenPopups.Remove(viewModel);

            _currentViewModel = viewModel;
            Activate(_currentViewModel);

            return true;
        }

        return false;
    }

    private T Create<T>() where T : IUIView
    {
        var uiView = GetPrefab<T>();
        var instantiate = _instantiater.Instantiate(uiView);

        return (T)instantiate;
    }

    private void Activate(UIViewModel viewModel)
    {
        var instantiatable = viewModel.GetInstantiatable();
        _instantiater.SetActive(instantiatable, true);
    }

    private void Deactivate(UIViewModel viewModel)
    {
        var popupDialog = viewModel.GetInstantiatable();
        _instantiater.SetActive(popupDialog, false);
    }

    private UIViewModel Destroy(UIViewModel viewModel)
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