using System;
using System.Collections.Generic;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.UISystem.Managers.UIWindowsLoader;
using Script.ProjectLibraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.UI.UIWindowsLoader
{
public class UnityUIWindowsLoader : IWindowsLoader
{
    private readonly IResourceLoader _resourceLoader;
    private List<IUIView> _windows;

    public UnityUIWindowsLoader(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
    }

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


    public void LoadDialogs(string bundleName)
    {
        _resourceLoader.LoadResource(bundleName, OnDialogsLoaded);
    }

    private void OnDialogsLoaded(GameObject[] dialogs)
    {
        var loadedWindows = new List<IUIView>(dialogs.Length);
        foreach (var gameObject in dialogs)
        {
            if (gameObject.TryGetComponent<IUIView>(out var component))
            {
                loadedWindows.Add(component);
            }
            else
            {
                Debug.LogError($"{gameObject.name} not have {typeof(IUIView)} component");
            }
        }
        
        _windows = new List<IUIView>(loadedWindows);

        if (_windows.Count == 0)
        {
            throw new Exception($"Need add prefabs in bundle");
        }
    }
}
}