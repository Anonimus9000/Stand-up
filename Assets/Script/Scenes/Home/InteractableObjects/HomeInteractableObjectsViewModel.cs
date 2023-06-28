using System;
using System.Collections.Generic;
using Script.DataServices.Base;
using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.ConfigParser.Configs.FakeConfigs;
using Script.ProjectLibraries.InputChecker.Base;
using Script.ProjectLibraries.InputChecker.MouseKeyboard;
using Script.ProjectLibraries.InputChecker.TouchScreen;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.Observer.DataObserver;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.Scenes.Common.ActionProgressSystem.Handler;
using Script.Scenes.Common.InteractableObjects.Bed;
using Script.Scenes.Common.InteractableObjects.Computer;
using Script.Scenes.Common.InteractableObjects.Toilet;
using Script.Scenes.Home.ActionProgressSystem.Handler;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Script.Scenes.Home.InteractableObjects
{
public class HomeInteractableObjectsViewModel : ViewModel
{
    private InputControls _inputControls;
    private DataObserver _observer;
    private readonly IDataService _dataService;
    private readonly IUIServiceLocator _uiServiceLocator;
    private readonly Canvas _canvas;
    private readonly IInteractableObjectsConfig _interactableObjectsFakeConfig;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;
    private readonly HomeInteractableObjectsView _view;
    private readonly IResourceLoader _resourceLoader;
    private readonly Camera _mainCamera;

    public HomeInteractableObjectsViewModel(
        IDataService dataService, 
        IUIServiceLocator uiServiceLocator,
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
        _uiServiceLocator = uiServiceLocator;
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
            _uiServiceLocator,
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
            _uiServiceLocator,
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