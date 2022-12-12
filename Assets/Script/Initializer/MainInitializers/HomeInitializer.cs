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

    [SerializeField]
    private Canvas _mainCanvas;

    private IServiceProvider _serviceProvider;

    public void Initialize()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        var uiManager = _monoDependencyContainers.GetDependency<IUISystem>();
        InitializeServiceProvider();
        
        InitializeHomeInteractableObjects(uiManager);
    }

    private void InitializeServiceProvider()
    {
        _serviceProvider = _monoDependencyContainers.GetDependency<IServiceProvider>();
    }

    private void InitializeHomeInteractableObjects(IUISystem iuiSystem)
    {
        var dataService = _serviceProvider.GetService<IDataService>();
        _homeInteractableObjectInitializer.InitializeDependencies(dataService, iuiSystem, _mainCanvas);
        var interactableViewModelsContainer = _homeInteractableObjectInitializer.Initialize();
    }
}
}