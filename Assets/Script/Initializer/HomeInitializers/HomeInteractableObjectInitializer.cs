using System;
using System.Collections.Generic;
using Script.ConfigData.LocationActionsConfig;
using Script.DataServices.Base;
using Script.Initializer.Base;
using Script.InputChecker.Base;
using Script.InputChecker.MouseKeyboard;
using Script.InteractableObject.ActionProgressSystem;
using Script.InteractableObject.Base;
using Script.InteractableObject.InteractableObjects.Container.Containers;
using Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Bed;
using Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Computer;
using Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Toilet;
using Script.Libraries.MVVM;
using Script.Libraries.Observer.DataObserver;
using Script.Libraries.UISystem.Managers.UiServiceProvider;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using UnityEngine;
#if UNITY_ANDROID
#endif

namespace Script.Initializer.HomeInitializers
{
public class HomeInteractableObjectInitializer : MonoBehaviour, IDependenciesInitializer
{
    [SerializeReference]
    private List<InteractableBase> _interactableObjects;

    private Camera _mainCamera;
    private InputControls _inputControls;
    private DataObserver _observer;
    private readonly List<IViewModel> _viewModels = new();
    private IDataService _dataService;
    private IUIServiceProvider _iuiSystem;
    private Canvas _canvas;
    private InteractableObjectsConfig _interactableObjectsConfig;
    private HomeActionProgressHandler _homeActionProgressHandler;

    public void InitializeDependencies(
        IDataService dataService, 
        IUIServiceProvider iuiSystem,
        Canvas mainCanvas,
        InteractableObjectsConfig interactableObjectsConfig,
        HomeActionProgressHandler homeActionProgressHandler)
    {
        _canvas = mainCanvas;
        _dataService = dataService;
        _iuiSystem = iuiSystem;
        _interactableObjectsConfig = interactableObjectsConfig;
        _homeActionProgressHandler = homeActionProgressHandler;
    }

    public IInitializable Initialize()
    {
        _observer = new DataObserver();

        InitializeInputControls();

        InitializeInteractableObjects();

        var interactableViewModelsContainer = new HomeInteractableObjectsManager(_viewModels.ToArray());

        return interactableViewModelsContainer;
    }

    private void InitializeInteractableObjects()
    {
        foreach (var interactableObject in _interactableObjects)
        {
            var interactableClickChecker = GetInteractableClickChecker(interactableObject.ClickTrackCollider);
            interactableObject.InitializeClickInput(interactableClickChecker);

            interactableObject.InitializeDependency(_observer, _canvas);

            if (interactableObject is IView interactableView)
            {
                var viewModel = InitializeViewModelByViewAndGet(interactableView, _dataService, _iuiSystem, _interactableObjectsConfig, _homeActionProgressHandler);
                _viewModels.Add(viewModel);
            }
            else
            {
                throw new Exception("Need serialize InteractableView");
            }
        }
    }

    private IObjectClickChecker GetInteractableClickChecker(Collider clickAreaCollider)
    {
 #if UNITY_EDITOR
         return new MouseLeftClickChecker(_mainCamera, _inputControls.MouseKeyboard.LeftButtonMousePress,
             clickAreaCollider);
#elif UNITY_ANDROID
        return new TouchClickChecker(_mainCamera, _inputControls.TochScreen.SingleTouch, clickAreaCollider);
#endif
    }

    private void InitializeInputControls()
    {
        _inputControls = new InputControls();
        _inputControls.Enable();
        _mainCamera = Camera.main;
    }

    private IViewModel InitializeViewModelByViewAndGet(
        IView view, 
        IDataService dataService, 
        IUIServiceProvider uiServiceProvider, 
        InteractableObjectsConfig interactableObjectsConfig,
        HomeActionProgressHandler homeActionProgressHandler)
    {
        var popupsUIService = uiServiceProvider.GetService<PopupsUIService>();
        var mainUIService = uiServiceProvider.GetService<MainUIService>();
        
        switch (view)
        {
            case ComputerView:
                return new ComputerViewModel(view, dataService, uiServiceProvider, interactableObjectsConfig, homeActionProgressHandler);
            case ToiletView:
                return new ToiletViewModel(view, dataService, uiServiceProvider, interactableObjectsConfig, homeActionProgressHandler);
            case BedView:
                break;
        }

        throw new Exception("Need add new interactable object in switch");
    }
}
}