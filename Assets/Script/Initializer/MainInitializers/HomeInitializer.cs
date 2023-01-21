using Script.ConfigData.LocationActionsConfig;
using Script.DataServices.Base;
using Script.Initializer.Base;
using Script.Initializer.HomeInitializers;
using Script.Initializer.MonoDependencyContainers;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.InteractableObject.ActionProgressSystem.Handler;
using Script.Libraries.ServiceProvider;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Initializer.MainInitializers
{
public class HomeInitializer : MonoBehaviour, IInitializer
{
    [SerializeReference]
    private HomeInteractableObjectInitializer _homeInteractableObjectInitializer;
    
    [FormerlySerializedAs("_monoDependencyContainers")]
    [SerializeField]
    private DependencyProviderBehaviour _dependencyContainers;

    [SerializeField]
    private Canvas _mainCanvas;

    //TODO: move to initializer
    [FormerlySerializedAs("_interactableObjectsData")]
    [SerializeField] private InteractableObjectsConfig _interactableObjectsConfig; 

    private IServiceProvider _serviceProvider;
    private readonly ActionProgressHandlerInitializer _actionProgressHandlerInitializer = new();

    public void Initialize()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        var uiManager = _dependencyContainers.GetDependency<IUIServiceProvider>();
        InitializeServiceProvider();
        var actionProgressHandler = InitializeActionProgressHandler();
        InitializeHomeInteractableObjects(uiManager, actionProgressHandler);
    }

    private void InitializeServiceProvider()
    {
        _serviceProvider = _dependencyContainers.GetDependency<IServiceProvider>();
    }

    private void InitializeHomeInteractableObjects(IUIServiceProvider iuiSystem, HomeActionProgressHandler actionProgressHandler)
    {
        var dataService = _serviceProvider.GetService<IDataService>();
        _homeInteractableObjectInitializer.InitializeDependencies(dataService, iuiSystem, _mainCanvas, _interactableObjectsConfig, actionProgressHandler);
        var interactableViewModelsContainer = _homeInteractableObjectInitializer.Initialize();
    }

    private HomeActionProgressHandler InitializeActionProgressHandler()
    {
        var homeActionProgressHandler = _actionProgressHandlerInitializer.Initialize() as HomeActionProgressHandler;

        return homeActionProgressHandler;
    }
}
}