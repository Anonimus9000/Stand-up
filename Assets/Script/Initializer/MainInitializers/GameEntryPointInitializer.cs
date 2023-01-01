using IngameDebugConsole;
using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.Initializer.StartApplicationDependenciesInitializers.UiInitializers;
using Script.InteractableObject.ActionProgressSystem;
using Script.Libraries.Logger.Loggers;
using Script.Libraries.ServiceProvider;
using Script.Libraries.UISystem.Managers.UiServiceProvider;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
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
    private UIServiceProviderInitializer _uiManagerInitializer;

    [FormerlySerializedAs("_monoDependencyProvider")]
    [SerializeField]
    private DependencyProviderBehaviour _dependencyProviderBehaviour;

    [SerializeField]
    private HomeInitializer _homeInitializer;
    
    [SerializeField] 
    private CharacterCreationData _characterCreationData;

    [SerializeField] private HomeActionProgressHandler _homeActionProgressHandler;

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
        _dependencyProviderBehaviour.AddDependency(_logger);

        var sceneSwitcher = InitializeSceneSwitcher(_sceneContainer, _homeInitializer, _homeGameObject, _logger);
        
        var uiManager = InitializeUISystem(sceneSwitcher);
        _dependencyProviderBehaviour.AddDependency(uiManager);


        var initializeDataServiceProvider = InitializeDataServiceProvider();
        _dependencyProviderBehaviour.AddDependency(initializeDataServiceProvider);
        
        OpenApplicationEnterDotWindow(uiManager, sceneSwitcher, _characterCreationData, _characterSelector, _homeActionProgressHandler);
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
        HomeActionProgressHandler homeActionProgressHandler)
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
                homeActionProgressHandler);
        mainUIService.Show<StartGameMenuView>(applicationEnterViewModel);
    }
}
}