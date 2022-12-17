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
    public UIViewModel Current { get; private set; }
    
    private readonly List<IUIView> _popupPrefabs;
    private readonly IInstantiater _instantiater;
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

        if (Current != null)
        {
            _queueHiddenPopups.Add(Current);
        }

        Current = viewModel;

        popupDialog!.SetUiManager(_iuiSystem);
        popupDialog!.OnShown();
        
        return popupDialog;
    }

    private bool TryHideCurrentPopup()
    {
        if(Current == null)
        {
            return false;
        }

        Deactivate(Current);

        return true;
    }

    public bool TryCloseCurrent()
    {
        if (Current == null)
        {
            return false;
        }

        Current = Destroy(Current);

        _queueHiddenPopups.Remove(Current);

        Current = null;

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

        Current = Destroy(Current);
    }

    private bool TryShowLastHiddenPopup()
    {
        if (_queueHiddenPopups.Count > 0)
        {
            var viewModel = _queueHiddenPopups.Last();
            _queueHiddenPopups.Remove(viewModel);

            Current = viewModel;
            Activate(Current);

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