using Script.DataServices.Base;
using Script.Initializer.Base;
using Script.Initializer.MonoDependencyContainers;
using Script.InteractableObject.InteractableObjects.Home.Initializer;
using Script.Libraries.ServiceProvider;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.UI.Dialogs.FullscreenDialogs.CharacterCreation.Components;
using Script.UI.Dialogs.PopupDialogs.Components;
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

    [SerializeField] private InteractableObjectsData _interactableObjectsData; 

    private IServiceProvider _serviceProvider;

    public void Initialize()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        var uiManager = _monoDependencyContainers.GetDependency<IUIServiceProvider>();
        InitializeServiceProvider();
        
        InitializeHomeInteractableObjects(uiManager);
    }

    private void InitializeServiceProvider()
    {
        _serviceProvider = _monoDependencyContainers.GetDependency<IServiceProvider>();
    }

    private void InitializeHomeInteractableObjects(IUIServiceProvider iuiSystem)
    {
        var dataService = _serviceProvider.GetService<IDataService>();
        _homeInteractableObjectInitializer.InitializeDependencies(dataService, iuiSystem, _mainCanvas, _interactableObjectsData);
        var interactableViewModelsContainer = _homeInteractableObjectInitializer.Initialize();
    }
}
}