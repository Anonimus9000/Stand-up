using IngameDebugConsole;
using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.Libraries.Logger.Loggers;
using Script.Libraries.ServiceProvider;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Container;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Converter;
using Script.UI.Dialogs.FullscreenDialogs.CharacterCreation.Components;
using Script.UI.Dialogs.MainUI.StartGameMenu;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;
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
    
    [SerializeField] 
    private CharacterCreationData _characterCreationData;

    [SerializeField] private ActionProgressHandler _actionProgressHandler;

    //TODO: replace
    [SerializeField]
    private GameObject _homeGameObject;

    [SerializeField]
    private CharacterSelector _characterSelector;

    [SerializeField]
    private Canvas _uiCanvas;

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
        
        var uiManager = InitializeUISystem(sceneSwitcher);
        _monoDependencyProvider.AddDependency(uiManager);


        var initializeDataServiceProvider = InitializeDataServiceProvider();
        _monoDependencyProvider.AddDependency(initializeDataServiceProvider);
        
        OpenApplicationEnterDotWindow(uiManager, sceneSwitcher, _characterCreationData, _characterSelector, _actionProgressHandler);
    }

    private IUIServiceProvider InitializeUISystem(ISceneSwitcher sceneSwitcher)
    {
        var uiManagerDependenciesInitializer = _uiManagerInitializer;
        uiManagerDependenciesInitializer.InitializeDependencies(sceneSwitcher);
        
        return (IUIServiceProvider)uiManagerDependenciesInitializer.Initialize();
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
    
    private void OpenApplicationEnterDotWindow(
        IUIServiceProvider uiServiceProvider, 
        ISceneSwitcher sceneSwitcher, 
        CharacterCreationData characterCreationData, 
        CharacterSelector characterSelector,
        ActionProgressHandler actionProgressHandler)
    {
        var fullScreensUIService = uiServiceProvider.GetService<FullScreensUIService>();
        var mainUIService = uiServiceProvider.GetService<MainUIService>();

        var positionsConverter = new PositionsConverter(_uiCanvas, Camera.main);
        
        var applicationEnterViewModel = 
            new StartGameMenuViewModel(
                mainUIService, 
                fullScreensUIService,
                sceneSwitcher, 
                characterCreationData,
                characterSelector,
                positionsConverter,
                actionProgressHandler);
        mainUIService.Show<StartGameMenuView>(applicationEnterViewModel);
    }
}
}