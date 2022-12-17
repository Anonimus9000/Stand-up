using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public class FullScreensManager : IDialogsManager
{
    private UIViewModel _current;
    private readonly List<IUIView> _fullScreenDialogPrefabs;
    private readonly IInstantiater _instantiater;
    private readonly List<UIViewModel> _queueHiddenScreens = new();
    private readonly IUISystem _iuiSystem;

    public FullScreensManager(IInstantiater instantiater, List<IUIView> mainUIPrefabs, IUISystem iuiSystem)
    {
        _iuiSystem = iuiSystem;
        _instantiater = instantiater;
        _fullScreenDialogPrefabs = mainUIPrefabs;
    }

    public T Show<T>(UIViewModel viewModel) where T : IUIView
    {
        _iuiSystem.CloseAllPopups();
        TryHideCurrentScreen();

        var screen = Create<T>();
        screen!.SetUiManager(_iuiSystem);
        screen!.OnShown();

        if (_current != null)
        {
            _queueHiddenScreens.Add(_current);
        }
        
        _current = viewModel;

        return screen;
    }

    public bool TryCloseCurrent()
    {
        if (_current == null)
        {
            return false;
        }
        
        _queueHiddenScreens.Remove(_current);
        
        _current = Destroy(_current);

        TryShowLastHiddenScreen();

        return true;
    }

    private UIViewModel Destroy(UIViewModel viewModel)
    {
        if (viewModel == null)
        {
            return null;
        }
        
        var instantiatable = viewModel.GetInstantiatable();
        _instantiater.Destroy(instantiatable);

        return null;
    }

    private bool TryHideCurrentScreen()
    {
        if (_current == null)
        {
            return false;
        }

        Deactivate(_current);

        _queueHiddenScreens.Add(_current);

        return true;
    }

    public void CloseAll()
    {
        foreach (var queueHiddenScreen in _queueHiddenScreens)
        {
            Destroy(queueHiddenScreen);
        }
        
        _queueHiddenScreens.Clear();

        Destroy(_current);
    }

    private bool TryShowLastHiddenScreen()
    {
        if (_queueHiddenScreens.Count > 0)
        {
            var viewModel = _queueHiddenScreens[^1];

            _queueHiddenScreens.Remove(viewModel);

            _current = viewModel;
            Activate(_current);

            return true;
        }

        return false;
    }

    private void Activate(UIViewModel viewModel)
    {
        var instantiatable = viewModel.GetInstantiatable();
        _instantiater.SetActive(instantiatable, true);
    }

    private void Deactivate(UIViewModel viewModel)
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