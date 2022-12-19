using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.UIWindowsLoader;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.BaseBehaviour;
using UnityEngine;

namespace Script.UI.UiWindowsLoader
{
public class UnityUIWindowsLoader : MonoBehaviour, IWindowsLoader
{
    public List<IUIView> UIWindows
    {
        get
        {
            if (_windows.Count == 0)
            {
                throw new Exception("Need load dialogs by method LoadDialogs()");
            }

            return _windows;
        }
    }

    private List<IUIView> _windows;

    public void LoadDialogs(string pathToLoad)
    {
        var loadedWindows = Resources.LoadAll<UiViewBehaviour>(pathToLoad);

        _windows = new List<IUIView>(loadedWindows);

        if (_windows.Count == 0)
        {
            throw new Exception($"Need add prefabs in {pathToLoad}");
        }
    }
}
}