using Script.DataServices.Base;
using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.InteractableObject.InteractableObjects.Home.Initializer;
using Script.Libraries.ServiceProvider;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Script.Initializer.MainInitializers
{
public class HomeInitializer : MonoBehaviour, IMainInitializer
{
    [SerializeReference]
    private HomeInteractableObjectInitializer _homeInteractableObjectInitializer;
    
    [SerializeField]
    private MonoDependencyProvider _monoDependencyContainers;

    //TODO:Move to scene manager
    [SerializeField] 
    private GameObject _homeObject;

    private IServiceProvider _serviceProvider;

    //TODO: Invoke from scene manager
    public void Initialize()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        var uiManager = _monoDependencyContainers.GetInitializable<IUIManager>();
        InitializeServiceProvider();
        
        InitializeHomeInteractableObjects(uiManager);
    }

    private void InitializeServiceProvider()
    {
        _serviceProvider = _monoDependencyContainers.GetInitializable<IServiceProvider>();
    }

    private void InitializeHomeInteractableObjects(IUIManager uiManager)
    {
        var dataService = _serviceProvider.GetService<IDataService>();
        _homeInteractableObjectInitializer.InitializeDependencies(dataService, uiManager);
        var interactableViewModelsContainer = _homeInteractableObjectInitializer.Initialize();
    }
}
}