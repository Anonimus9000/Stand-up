using System;
using System.Collections.Generic;
using System.Linq;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public class FullScreensManager : IDialogsManager
{
    private IFullScreen _current;
    private List<IUIWindow> _fullScreenDialogPrefabs;
    private IInstantiater _instantiater;
    private readonly List<IFullScreen> _queueHiddenScreens = new List<IFullScreen>();
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

        var screen = _instantiater.Instantiate(screenToShow) as IFullScreen;

        _current = screen;

        screen!.OnShown();
        screen!.InitializeWindow(_uiManager);
        _queueHiddenScreens.Add(screen);

        return screen;
    }

    private bool TryHideCurrentScreen()
    {
        if (_current == null)
        {
            return false;
        }

        _instantiater.SetActive(_current, false);

        return true;
    }

    public void Close<T>() where T : IUIWindow
    {
        if (_queueHiddenScreens.Count == 0)
        {
            return;
        }
        var screen = _queueHiddenScreens.Last();
        _queueHiddenScreens.Remove(screen);
        _instantiater.Destroy(screen);
        _current = null;

        TryShowLastHiddenScreen();
    }

    private bool TryShowLastHiddenScreen()
    {
        if (_queueHiddenScreens.Count > 0)
        {
            var screen = _queueHiddenScreens.Last();
            _instantiater.SetActive(screen, true);
            _current = screen;

            return true;
        }

        return false;
    }

    private IFullScreen GetPrefab<T>() where T : IUIWindow
    {
        foreach (var dialogPrefab in _fullScreenDialogPrefabs)
        {
            if (dialogPrefab is T)
            {
                return dialogPrefab as IFullScreen;
            }
        }

        throw new Exception($"Can't find prefab {typeof(T)}");
    }
}
}