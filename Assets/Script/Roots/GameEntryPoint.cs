using Plugins.IngameDebugConsole.Scripts;
using Script.DataServices.Base;
using Script.DataServices.Services.PlayerDataService;
using Script.Initializer.Base;
using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.CharacterCreationData;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.InGameEventsData;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.LocationActionsData;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.PlayerDataData;
using Script.ProjectLibraries.ConfigParser.Parsers.Fake;
using Script.ProjectLibraries.Logger.Loggers;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.SceneSwitcherSystem;
using Script.ProjectLibraries.ServiceLocators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider;
using Script.ResourceLoader;
using Script.Scenes;
using Script.Scenes.MainMenu.UIs.FullScreens.CharacterCreation.Components;
using Script.Scenes.MainMenu.UIs.MainUIs.StartGameMenu;
using Script.UI.Root;
using Script.Utils.UIPositionConverter;
using UnityEngine;
using UnityEngine.Serialization;
using ILogger = Script.ProjectLibraries.Logger.LoggerBase.ILogger;
using IUIServiceLocator = Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider.IUIServiceLocator;

namespace Script.Roots
{
public class GameEntryPoint : BehaviourDisposableBase, IRoot
{
    #region MonoBehavioursDependencies

    [SerializeField]
    private UIRoot _uiRoot;

    #region FakeConfigs

    [SerializeField] 
    private CharacterCreationData _characterCreationData;

    [FormerlySerializedAs("_inActionProgressEventsFakeConfig")]
    [SerializeField]
    private InActionProgressEventsFakeConfigData _inActionProgressEventsFakeConfigData;
    
    [FormerlySerializedAs("_playerDataFakeConfig")]
    [SerializeField]
    private PlayerDataFakeConfigData _playerDataFakeConfigData;

    [FormerlySerializedAs("_interactableObjectsFakeConfig")]
    [SerializeField]
    private InteractableObjectsFakeConfigData _interactableObjectsFakeConfigData;
    
    #endregion

    [SerializeField]
    private CharacterSelector _characterSelector;

    [SerializeField]
    private Canvas _uiCanvas;

    [SerializeField]
    private Transform _scenesParent;

    [SerializeField]
    private FakeResourceBundleContainer _resourceBundleContainer;

    [SerializeField] 
    private DebugLogManager _inGameDebugConsole;

    #endregion

    private void Awake()
    {
        InitializeElements();
    }

    private void InitializeElements()
    {
        Instantiate(_inGameDebugConsole, transform);
        var logger = InitializeLogger();
        var mainCamera = Camera.main;
        var mainConfig = ParseMainConfig(
            _characterCreationData,
            _inActionProgressEventsFakeConfigData,
            _playerDataFakeConfigData,
            _interactableObjectsFakeConfigData);
        
        var resourceLoader = new FakeBundleResourceLoader(_resourceBundleContainer);
        var uiServiceLocator = InitializeUISystem(logger, resourceLoader);
        var positionsConverter = new PositionsConverter(_uiCanvas, Camera.main);

        var dataServiceServiceLocator = InitializeDataServiceProvider(mainConfig.PlayerDataConfig);
        
        var sceneSwitcher = InitializeSceneSwitcher(
            logger,
            resourceLoader,
            _scenesParent,
            mainConfig.InActionProgressConfig,
            uiServiceLocator,
            mainConfig.CharacterModelsConfig,
            positionsConverter,
            dataServiceServiceLocator.GetService<PlayerDataService>(), 
            mainConfig.FakeInteractableObjectsConfig, 
            _uiCanvas, 
            dataServiceServiceLocator,
            mainCamera);


        OpenApplicationEnterDotWindow(
            uiServiceLocator,
            sceneSwitcher,
            mainConfig.CharacterModelsConfig,
            dataServiceServiceLocator,
            positionsConverter,
            resourceLoader);
    }

    private IMainConfig ParseMainConfig(
        CharacterCreationData characterCreationData,
        InActionProgressEventsFakeConfigData inActionProgressEventsFakeConfigData,
        PlayerDataFakeConfigData playerDataFakeConfigData,
        InteractableObjectsFakeConfigData interactableObjectsFakeConfigData)
    {
        var parser = new FakeMainConfigParser(
            inActionProgressEventsFakeConfigData,
            playerDataFakeConfigData,
            characterCreationData, 
            interactableObjectsFakeConfigData);
        var mainConfig = parser.ParseMainConfig();
        
        return mainConfig;
    }

    private IUIServiceLocator InitializeUISystem(ILogger logger, IResourceLoader resourceLoader)
    {
        return _uiRoot.Initialize(logger, resourceLoader);
    }

    private ISceneSwitcher InitializeSceneSwitcher(ILogger logger,
        IResourceLoader resourceLoader,
        Transform scenesParent,
        IInActionProgressConfig inActionProgressEventsFakeConfig,
        IUIServiceLocator uiServiceLocator,
        ICharacterModelsConfig characterConfig,
        PositionsConverter positionsConverter,
        IDataService playerData,
        IInteractableObjectsConfig interactableObjectsConfig,
        Canvas mainCanvas, 
        IDataServiceLocator dataServiceLocator,
        Camera mainCamera)
    {
        var sceneContainer = new SceneModel(logger,
            resourceLoader,
            scenesParent,
            inActionProgressEventsFakeConfig,
            uiServiceLocator,
            characterConfig,
            positionsConverter,
            playerData,
            interactableObjectsConfig,
            mainCanvas,
            dataServiceLocator, 
            mainCamera);
        
        compositeDisposable.AddDisposable(sceneContainer);
        return sceneContainer.SceneSwitcher;
    }

    private IDataServiceLocator InitializeDataServiceProvider(IPlayerDataConfig playerDataConfig)
    {
        var dataServiceProviderInitializer = new DataServiceProviderRoot(playerDataConfig);

        return dataServiceProviderInitializer.Initialize();
    }

    private static ILogger InitializeLogger()
    {
        var logger = new UnityMainLogger();

        return logger;
    }
    
    private void OpenApplicationEnterDotWindow(
        IUIServiceLocator iuiServiceLocator, 
        ISceneSwitcher sceneSwitcher, 
        ICharacterModelsConfig characterConfig, 
        IDataServiceLocator dataServiceLocator,
        PositionsConverter positionsConverter,
        IResourceLoader resourceLoader)
    {
        var playerDataService = dataServiceLocator.GetService<PlayerDataService>();

        var applicationEnterViewModel = new StartGameMenuViewModel(
            iuiServiceLocator,
            sceneSwitcher,
            characterConfig,
            positionsConverter,
            playerDataService,
            resourceLoader);
        
        compositeDisposable.AddDisposable(applicationEnterViewModel);
        
        var mainUIService = iuiServiceLocator.GetService<MainUIService>();
        mainUIService.Show<StartGameMenuView>(applicationEnterViewModel);
    }
}
}