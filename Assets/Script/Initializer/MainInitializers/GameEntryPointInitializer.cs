using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.Libraries.Logger.Loggers;
using Script.Libraries.ServiceProvider;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Container;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Dialogs.FullscreenDialogs;
using Script.UI.Dialogs.FullscreenDialogs.ApplicationEnter;
using Script.UI.Dialogs.FullscreenDialogs.StartGameMenu;
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

        var sceneSwitcher = InitializeSceneSwitcher(_sceneContainer, _homeInitializer, _homeGameObject, _logger);
        
        var uiManager = InitializeUISystem();
        _monoDependencyProvider.AddDependency(uiManager);


        var initializeDataServiceProvider = InitializeDataServiceProvider();
        _monoDependencyProvider.AddDependency(initializeDataServiceProvider);
        
        OpenApplicationEnterDotWindow(uiManager, sceneSwitcher);
    }

    private IUISystem InitializeUISystem()
    {
        return (IUISystem)_uiManagerInitializer.Initialize();
    }

    private static ISceneSwitcher InitializeSceneSwitcher(ISceneContainer sceneContainer,
        IInitializer homeInitializer, GameObject homeGameObject, ILogger logger)
    {
        var sceneSwitcher = new SceneSwitcherDependenciesInitializer(
            sceneContainer, homeInitializer, homeGameObject, logger);

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
    
    private void OpenApplicationEnterDotWindow(IUISystem iuiSystem, ISceneSwitcher sceneSwitcher)
    {
        var applicationEnterViewModel = new StartGameMenuViewModel(iuiSystem, sceneSwitcher);
        iuiSystem.Show(applicationEnterViewModel);
    }
}
}