using Script.DataServices.Base;
using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.InteractableObject.InteractableObjects.Home.Initializer;
using Script.Libraries.ServiceProvider;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Initializer.MainInitializers
{
public class HomeInitializer : MonoBehaviour, IMainInitializer
{
    [SerializeReference]
    private HomeInteractableObjectInitializer _homeInteractableObjectInitializer;
    
    [FormerlySerializedAs("_monoDependancyContainers")]
    [FormerlySerializedAs("_monoInitializableContainers")]
    [SerializeField]
    private MonoDependencyProvider _monoDependencyContainers;

    private IServiceProvider _serviceProvider;

    private void OnEnable()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        InitializeServiceProvider();
        
        InitializeHomeInteractableObjects();
    }

    private void InitializeServiceProvider()
    {
        _serviceProvider = _monoDependencyContainers.GetInitializable<IServiceProvider>();
    }

    private void InitializeHomeInteractableObjects()
    {
        var dataService = _serviceProvider.GetService<IDataService>();
        _homeInteractableObjectInitializer.InitializeDependencies(dataService);
        var interactableViewModelsContainer = _homeInteractableObjectInitializer.Initialize();
    }
}
}