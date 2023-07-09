using Script.DataServices.Base;
using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.Root;
using Script.ProjectLibraries.ServiceLocators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.Scenes.Home.ActionProgressSystem.Handler;
using Script.Scenes.Home.InteractableObjects;
using UnityEngine;

namespace Script.Scenes.Home
{
//TODO: make as MVVM, remove root
public class HomeRoot : BehaviourDisposableBase, IRoot
{
    [SerializeField]
    private HomeInteractableObjectsView _homeInteractableObjectsView; 

    private Camera _mainCamera;

    public void Initialize(
        IUIServiceProvider iuiServiceProvider,
        IDataServiceLocator dataServiceLocator,
        HomeActionProgressHandler homeActionProgressHandler,
        Canvas mainCanvas,
        IInteractableObjectsConfig interactableObjectsConfig,
        IResourceLoader resourceLoader,
        Camera mainCamera)
    {
        _mainCamera = mainCamera;
        InitializeHomeInteractableObjects(
            iuiServiceProvider,
            homeActionProgressHandler,
            dataServiceLocator.GetService<IDataService>(),
            mainCanvas,
            interactableObjectsConfig,
            resourceLoader);
    }
    
    private void InitializeHomeInteractableObjects(
        IUIServiceProvider iuiSystem,
        HomeActionProgressHandler actionProgressHandler,
        IDataService dataService,
        Canvas mainCanvas,
        IInteractableObjectsConfig interactableObjectsFakeConfig,
        IResourceLoader resourceLoader)
    {
        compositeDisposable.AddDisposable(new HomeInteractableObjectsViewModel(
            dataService,
            iuiSystem,
            mainCanvas,
            interactableObjectsFakeConfig,
            actionProgressHandler,
            resourceLoader,
            _homeInteractableObjectsView,
            _mainCamera));
    }
}
}