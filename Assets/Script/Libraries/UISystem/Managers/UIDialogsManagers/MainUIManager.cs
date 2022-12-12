using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public class MainUIManager : IDialogsManager
{
    private readonly IUISystem _iuiSystem;
    private readonly IInstantiater _instantiater;
    private readonly List<IUIView> _mainUIPrefabs;
    private UIViewModel _current;

    public MainUIManager(IInstantiater instantiater, List<IUIView> mainUIPrefabs, IUISystem iuiSystem)
    {
        _iuiSystem = iuiSystem;
        _instantiater = instantiater;
        _mainUIPrefabs = mainUIPrefabs;
    }

    public T Show<T>(UIViewModel viewModel) where T : IUIView
    {
        _iuiSystem.CloseWindowsExceptMain();

        TryCloseCurrent();

        var mainUI = Create<T>();

        _current = viewModel;

        mainUI!.OnShown();
        mainUI!.SetUiManager(_iuiSystem);

        return mainUI;
    }

    public void CloseAll()
    {
        TryCloseCurrent();
    }

    public bool TryCloseCurrent()
    {
        if (_current == null)
        {
            return false;
        }

        _current = Destroy(_current);

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