using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public class MainUIManager: IDialogsManager
{
    private IUIManager _uiManager;
    private IInstantiater _instantiater;
    private List<IUIWindow> _mainUIPrefabs;
    private IMainUI _currentMainUI;

    public void Initialize(IInstantiater instantiater, List<IUIWindow> mainUI, IUIManager uiManager)
    {
        _uiManager = uiManager;
        _instantiater = instantiater;

        _mainUIPrefabs = mainUI;
    }

    public T Show<T>() where T : IUIWindow
    {
        _uiManager.CloseWindowsExceptMain();
        
        TryCloseCurrentMainUI();

        var mainUIToShow = GetPrefab<T>();

        var mainUI = _instantiater.Instantiate(mainUIToShow) as IMainUI;
        
        _currentMainUI = mainUI;
        
        mainUI!.OnShown();
        mainUI!.SetUiManager(_uiManager);

        return (T) mainUI;
    }

    private IInstantiatable GetPrefab<T>() where T : IUIWindow
    {
        foreach (var dialogPrefab in _mainUIPrefabs)
        {
            if (dialogPrefab is T)
            {
                return dialogPrefab as IMainUI;
            }
        }

        throw new Exception($"Can't find prefab {typeof(T)}");
    }

    private bool TryCloseCurrentMainUI()
    {
        if (_currentMainUI is null)
        {
            return false;
        }
        _instantiater.Destroy(_currentMainUI);
        
        return true;
    }

    public void Close<T>() where T : IUIWindow
    {
        _instantiater.Destroy(_currentMainUI);
    }
}
}