using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.UIWindowsLoader;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;

namespace Script.UI.UiWindowsLoader
{
public class UnityUIWindowsLoader : MonoBehaviour, IWindowsLoader
{
    public List<IUIWindow> UIWindows
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

    private List<IUIWindow> _windows;

    public void LoadDialogs(string pathToLoad)
    {
        var loadedWindows = Resources.LoadAll<UIWindowViewBase>(pathToLoad);

        _windows = new List<IUIWindow>(loadedWindows);

        if (UIWindows.Count == 0)
        {
            throw new Exception($"Need add prefabs in {pathToLoad}");
        }
    }
}
}