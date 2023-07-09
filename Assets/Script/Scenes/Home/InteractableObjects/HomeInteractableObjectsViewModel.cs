using Script.DataServices.Base;
using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.Observer.DataObserver;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.Scenes.Common.InteractableObjects.Computer;
using Script.Scenes.Common.InteractableObjects.Toilet;
using Script.Scenes.Home.ActionProgressSystem.Handler;
using UnityEngine;

namespace Script.Scenes.Home.InteractableObjects
{
public class HomeInteractableObjectsViewModel : ViewModel
{
    private InputControls _inputControls;
    private DataObserver _observer;
    private readonly IDataService _dataService;
    private readonly IUIServiceProvider _iuiServiceProvider;
    private readonly Canvas _canvas;
    private readonly IInteractableObjectsConfig _interactableObjectsFakeConfig;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;
    private readonly HomeInteractableObjectsView _view;
    private readonly IResourceLoader _resourceLoader;
    private readonly Camera _mainCamera;

    public HomeInteractableObjectsViewModel(
        IDataService dataService, 
        IUIServiceProvider iuiServiceProvider,
        Canvas mainCanvas,
        IInteractableObjectsConfig interactableObjectsFakeConfig,
        HomeActionProgressHandler homeActionProgressHandler,
        IResourceLoader resourceLoader, 
        HomeInteractableObjectsView view,
        Camera mainCamera)
    {
        _mainCamera = mainCamera;
        _canvas = mainCanvas;
        _dataService = dataService;
        _view = AddDisposable(view);
        _iuiServiceProvider = iuiServiceProvider;
        _interactableObjectsFakeConfig = interactableObjectsFakeConfig;
        _homeActionProgressHandler = homeActionProgressHandler;
        _resourceLoader = resourceLoader;
        _observer = new DataObserver();
        InitializeInputControls();
        InitializeInteractableObjects();
    }
    
    private void InitializeInteractableObjects()
    {
        InitializeToilet(_view.ToiletParent);
        InitializeComputer(_view.ComputerParent);
    }

    private void InitializeComputer(Transform parent)
    {
        AddDisposable(new ComputerViewModel(
            _iuiServiceProvider,
            _interactableObjectsFakeConfig,
            _homeActionProgressHandler,
            _observer,
            _canvas,
            _resourceLoader,
            parent,
            _mainCamera,
            _inputControls));
    }

    private void InitializeToilet(Transform parent)
    {
        AddDisposable(new ToiletViewModel(
            _iuiServiceProvider,
            _interactableObjectsFakeConfig,
            _homeActionProgressHandler,
            _resourceLoader,
            parent,
            _observer, 
            _mainCamera, 
            _inputControls, 
            _canvas));
    }

    private void InitializeInputControls()
    {
        _inputControls = new InputControls();
        _inputControls.Enable();
    }
}
}