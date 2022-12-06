using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.Libraries.ServiceProvider;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Container;
using Script.SceneSwitcherSystem.Switcher;
using UnityEngine;

namespace Script.Initializer.MainInitializers
{
public class GameEntryPointInitializer : MonoBehaviour, IMainInitializer
{
    #region MonoBehavioursDependencies

    [SerializeField]
    private UIManagerDependenciesInitializer _uiManagerInitializer;

    [SerializeField]
    private SceneContainer _sceneContainer;

    [SerializeField]
    private MonoDependencyProvider _monoDependencyProvider;

    //TODO: Replace in SceneManager
    [SerializeField] 
    private HomeInitializer _homeInitializer;

    #endregion

    private void OnEnable()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        var uiManager = InitializeUISystem();
        var sceneSwitcher = InitializeSceneSwitcher(uiManager);
        var initializeDataServiceProvider = InitializeDataServiceProvider();
        
        _monoDependencyProvider.InitializeDependencies(initializeDataServiceProvider, uiManager);
    }

    private IUIManager InitializeUISystem()
    {
        _uiManagerInitializer.StartGamePressed += _homeInitializer.Initialize;
        return (IUIManager)_uiManagerInitializer.Initialize();
    }

    private ISceneSwitcher InitializeSceneSwitcher(IUIManager uiManager)
    {
        var sceneSwitcher = new SceneSwitcherDependenciesInitializer(uiManager, _sceneContainer);
        return (ISceneSwitcher)sceneSwitcher.Initialize();
    }

    private IServiceProvider InitializeDataServiceProvider()
    {
        var dataServiceProviderInitializer = new DataServiceProviderInitializer();
        return (IServiceProvider)dataServiceProviderInitializer.Initialize();
    } 
}
}