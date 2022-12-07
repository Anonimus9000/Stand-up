using Script.DataServices.Base;
using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.InteractableObject.InteractableObjects.Home.Initializer;
using Script.Libraries.ServiceProvider;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using UnityEngine;

namespace Script.Initializer.MainInitializers
{
public class HomeInitializer : MonoBehaviour, IInitializer
{
    [SerializeReference]
    private HomeInteractableObjectInitializer _homeInteractableObjectInitializer;
    
    [SerializeField]
    private MonoDependencyProvider _monoDependencyContainers;

    private IServiceProvider _serviceProvider;

    public void Initialize()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        var uiManager = _monoDependencyContainers.GetDependency<IUIManager>();
        InitializeServiceProvider();
        
        InitializeHomeInteractableObjects(uiManager);
    }

    private void InitializeServiceProvider()
    {
        _serviceProvider = _monoDependencyContainers.GetDependency<IServiceProvider>();
    }

    private void InitializeHomeInteractableObjects(IUIManager uiManager)
    {
        var dataService = _serviceProvider.GetService<IDataService>();
        _homeInteractableObjectInitializer.InitializeDependencies(dataService, uiManager);
        var interactableViewModelsContainer = _homeInteractableObjectInitializer.Initialize();
    }
}
}