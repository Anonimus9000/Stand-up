using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.Libraries.Logger.Loggers;
using Script.Libraries.ServiceProvider;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Container;
using Script.SceneSwitcherSystem.Container.Scenes;
using Script.SceneSwitcherSystem.Switcher;
using UnityEngine;
using ILogger = Script.Libraries.Logger.LoggerBase.ILogger;

namespace Script.Initializer.MainInitializers
{
public class GameEntryPointInitializer : MonoBehaviour, IMainInitializer
{
    #region MonoBehavioursDependencies

    [SerializeField]
    private UIManagerDependenciesInitializer _uiManagerInitializer;

    [SerializeField]
    private MonoDependencyProvider _monoDependencyProvider;

    [SerializeField]
    private HomeInitializer _homeInitializer;

    //TODO: replace
    [SerializeField]
    private GameObject _homeGameObject;

    #endregion

    private SceneContainer _sceneContainer;
    private ILogger _logger;

    private void OnEnable()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        _logger = InitializeLogger();
        _monoDependencyProvider.AddDependency(_logger);

        var uiManager = InitializeUISystem();
        _monoDependencyProvider.AddDependency(uiManager);

        var sceneSwitcher =
            InitializeSceneSwitcher(uiManager, _sceneContainer, _homeInitializer, _homeGameObject, _logger);
        sceneSwitcher.SwitchTo<ApplicationLoadingScene>();

        var initializeDataServiceProvider = InitializeDataServiceProvider();
        _monoDependencyProvider.AddDependency(initializeDataServiceProvider);
    }

    private IUIManager InitializeUISystem()
    {
        return (IUIManager)_uiManagerInitializer.Initialize();
    }

    private static ISceneSwitcher InitializeSceneSwitcher(IUIManager uiManager, ISceneContainer sceneContainer,
        IInitializer homeInitializer, GameObject homeGameObject, ILogger logger)
    {
        var sceneSwitcher = new SceneSwitcherDependenciesInitializer(
            uiManager, sceneContainer, homeInitializer, homeGameObject, logger);

        return (ISceneSwitcher)sceneSwitcher.Initialize();
    }

    private static IServiceProvider InitializeDataServiceProvider()
    {
        var dataServiceProviderInitializer = new DataServiceProviderInitializer();

        return (IServiceProvider)dataServiceProviderInitializer.Initialize();
    }

    private static ILogger InitializeLogger()
    {
        var logger = new UnityMainLogger();

        return logger;
    }
}
}