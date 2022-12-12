using System.Collections.Generic;
using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Dialogs.FullscreenDialogs;
using Script.UI.System;
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

    [field: SerializeField]
    public Canvas MainCanvas { get; private set; }

    private ISceneSwitcher _sceneSwitcher;

    public IInitializable Initialize()
    {
        var logger = _monoDependencyProvider.GetDependency<ILogger>();
        var uiManager = InitializeUIManager(logger);

        return uiManager;
    }

    public void InitializeDependencies(ISceneSwitcher sceneSwitcher)
    {
        _sceneSwitcher = sceneSwitcher;
    }

    private MonoUiSystem InitializeUIManager(ILogger logger)
    {
        var uiWindows = InitializeWindowsLoader();

        IInstantiater instantiaterMainUI = new UnityInstantiater(_parentToCreateMainUI);
        IInstantiater instantiaterFullScreens = new UnityInstantiater(_parentToCreateFullScreenUI);
        IInstantiater instantiaterPopups = new UnityInstantiater(_parentToCreatePopupsUI);

        return new MonoUiSystem(
            instantiaterMainUI,
            instantiaterFullScreens,
            instantiaterPopups,
            uiWindows,
            logger);
    }

    private List<IUIView> InitializeWindowsLoader()
    {
        var loaderObject = new GameObject("WindowsLoader");
        loaderObject.transform.SetParent(transform);
        var unityUIWindowsLoader = loaderObject.AddComponent<UnityUIWindowsLoader>();

        unityUIWindowsLoader.LoadDialogs(_pathToDialogs);

        return unityUIWindowsLoader.UIWindows;
    }
}
}