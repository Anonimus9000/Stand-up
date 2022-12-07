using System.Collections.Generic;
using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.FullscreenDialogs;
using Script.UI.Manager;
using Script.UI.UIInstantiater;
using Script.UI.UiWindowsLoader;
using UnityEngine;
using ILogger = Script.Libraries.Logger.LoggerBase.ILogger;

namespace Script.Initializer.StartApplicationDependenciesInitializers
{
public class UIManagerDependenciesInitializer : MonoBehaviour, IDependenciesInitializer
{
    [SerializeField]
    private Transform _parentToCreateMainUI;

    [SerializeField]
    private Transform _parentToCreateFullScreenUI;

    [SerializeField]
    private Transform _parentToCreatePopupsUI;

    [SerializeField]
    private string _pathToDialogs;

    [SerializeField]
    private MonoDependencyProvider _monoDependencyProvider;

    public IInitializable Initialize()
    {
        var logger = _monoDependencyProvider.GetDependency<ILogger>();
        var uiManager = InitializeUIManager(logger);

        OpenApplicationEnterDotWindow(uiManager);

        return uiManager;
    }

    private UIManagerInitializable InitializeUIManager(ILogger logger)
    {
        var uiWindows = InitializeWindowsLoader();

        IInstantiater instantiaterMainUI = new UnityInstantiater(_parentToCreateMainUI);
        IInstantiater instantiaterFullScreens = new UnityInstantiater(_parentToCreateFullScreenUI);
        IInstantiater instantiaterPopups = new UnityInstantiater(_parentToCreatePopupsUI);

        return new UIManagerInitializable(instantiaterMainUI, instantiaterFullScreens, instantiaterPopups, uiWindows,
            logger);
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
        var uiWindow = uiManager.Show<StartFullScreen>();
    }
}
}