using System;
using System.Collections.Generic;
using System.Linq;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public class FullScreensManager : IDialogsManager
{
    private IFullScreenDialog _currentDialog;
    private List<IUIWindow> _fullScreenDialogPrefabs;
    private IInstantiater _instantiater;
    private readonly List<IFullScreenDialog> _queueHiddenScreens = new List<IFullScreenDialog>();
    private IUIManager _uiManager;

    public void Initialize(IInstantiater instantiater, List<IUIWindow> fullScreenDialogs, IUIManager uiManager)
    {
        _uiManager = uiManager;
        _instantiater = instantiater;

        _fullScreenDialogPrefabs = fullScreenDialogs;
    }

    public IUIWindow Show<T>() where T : IUIWindow
    {
        TryHideCurrentScreen();

        var screenToShow = GetPrefab<T>();

        var screen = _instantiater.Instantiate(screenToShow) as IFullScreenDialog;

        _currentDialog = screen;

        screen!.OnShown();
        screen!.InitializeWindow(_uiManager);
        _queueHiddenScreens.Add(screen);

        return screen;
    }

    private bool TryHideCurrentScreen()
    {
        if (_currentDialog is null)
        {
            return false;
        }

        _instantiater.SetActive(_currentDialog, false);

        return true;
    }

    public void Close<T>() where T : IUIWindow
    {
        var screen = _queueHiddenScreens.Last();
        _queueHiddenScreens.Remove(screen);
        _instantiater.Destroy(screen);

        TryShowLastHiddenScreen();
    }

    private bool TryShowLastHiddenScreen()
    {
        if (_queueHiddenScreens.Count > 0)
        {
            var screen = _queueHiddenScreens.Last();
            _instantiater.SetActive(screen, true);
            _currentDialog = screen;

            return true;
        }

        return false;
    }

    private IFullScreenDialog GetPrefab<T>() where T : IUIWindow
    {
        foreach (var dialogPrefab in _fullScreenDialogPrefabs)
        {
            if (dialogPrefab is T)
            {
                return dialogPrefab as IFullScreenDialog;
            }
        }

        throw new Exception($"Can't find prefab {typeof(T)}");
    }
}
}