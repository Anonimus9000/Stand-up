using System.Collections.Generic;
using Script.Initializer.Base;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Manager;
using Script.UI.Tests;
using Script.UI.UIInstantiater;
using Script.UI.UiWindowsLoader;
using UnityEngine;

namespace Script.Initializer.StartApplicationDependenciesInitializers
{
public class UIManagerDependenciesInitializer : MonoBehaviour, IDependenciesInitializer
{
    [SerializeField] private Transform _parentToCreateMainUI;
    [SerializeField] private Transform _parentToCreateFullScreenUI;
    [SerializeField] private Transform _parentToCreatePopupsUI;
    [SerializeField] private string _pathToDialogs;

    public IInitializable Initialize()
    {
        var uiManager = InitializeUIManager();

        OpenApplicationEnterDotWindow(uiManager);

        return uiManager;
    }

    private UIManagerInitializable InitializeUIManager()
    {
        var uiWindows = InitializeWindowsLoader();

        IInstantiater instantiaterMainUI = new UnityInstantiater(_parentToCreateMainUI);
        IInstantiater instantiaterFullScreens = new UnityInstantiater(_parentToCreateFullScreenUI);
        IInstantiater instantiaterPopups = new UnityInstantiater(_parentToCreatePopupsUI);
        
        return new UIManagerInitializable(instantiaterMainUI, instantiaterFullScreens, instantiaterPopups, uiWindows);
    }
    

    private List<IUIWindow> InitializeWindowsLoader()
    {
        var loaderObject = new GameObject("WindowsLoader");
        loaderObject.transform.SetParent(transform);
        var unityUIWindowsLoader = loaderObject.AddComponent<UnityUIWindowsLoader>();

        unityUIWindowsLoader.LoadDialogs(_pathToDialogs);

        return unityUIWindowsLoader.UIWindows;
    }

    private void OpenApplicationEnterDotWindow(IUIManager uiManager)
    {
        uiManager.Show<TestMainUI1>();
        uiManager.Show<TestScreen1>();
    }
}
}