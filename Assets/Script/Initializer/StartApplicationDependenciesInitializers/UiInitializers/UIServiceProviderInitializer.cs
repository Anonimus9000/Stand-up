using System.Collections.Generic;
using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.System;
using Script.UI.UIInstantiater;
using Script.UI.UiWindowsLoader;
using UnityEngine;
using UnityEngine.Serialization;
using ILogger = Script.Libraries.Logger.LoggerBase.ILogger;

namespace Script.Initializer.StartApplicationDependenciesInitializers.UiInitializers
{
public class UIServiceProviderInitializer : MonoBehaviour, IDependenciesInitializer
{
    [SerializeField]
    private Transform _parentToCreateMainUI;

    [SerializeField]
    private Transform _parentToCreateFullScreenUI;

    [SerializeField]
    private Transform _parentToCreatePopupsUI;

    [SerializeField]
    private string _pathToDialogs;

    [FormerlySerializedAs("_monoDependencyProvider")]
    [SerializeField]
    private DependencyProviderBehaviour _dependencyProviderBehaviour;

    [FormerlySerializedAs("MainCanvas")]
    [SerializeField]
    private Canvas _mainCanvas;

    [SerializeField]
    private UiAnimatorInitializer _animatorInitializer;

    public Canvas MainCanvas => _mainCanvas;

    private ISceneSwitcher _sceneSwitcher;

    public IInitializable Initialize()
    {
        var logger = _dependencyProviderBehaviour.GetDependency<ILogger>();
        var initializeAnimatorServiceProvider = _animatorInitializer.InitializeAnimatorServiceProvider();
        var uiManager = InitializeUIManager(logger, _sceneSwitcher, initializeAnimatorServiceProvider);

        return uiManager;
    }

    public void InitializeDependencies(ISceneSwitcher sceneSwitcher)
    {
        _sceneSwitcher = sceneSwitcher;
    }

    private UiServiceProviderInitializable InitializeUIManager(
        ILogger logger,
        ISceneSwitcher sceneSwitcher,
        IAnimatorServiceProvider animatorServiceProvider)
    {
        var uiWindows = InitializeWindowsLoader();

        IInstantiater instantiaterMainUI = new UnityInstantiater(_parentToCreateMainUI);
        IInstantiater instantiaterFullScreens = new UnityInstantiater(_parentToCreateFullScreenUI);
        IInstantiater instantiaterPopups = new UnityInstantiater(_parentToCreatePopupsUI);

        return new UiServiceProviderInitializable(
            instantiaterMainUI,
            instantiaterFullScreens,
            instantiaterPopups,
            uiWindows,
            logger,
            sceneSwitcher, 
            animatorServiceProvider);
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