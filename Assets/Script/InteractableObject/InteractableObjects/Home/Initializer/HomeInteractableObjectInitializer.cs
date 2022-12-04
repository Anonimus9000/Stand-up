using System;
using System.Collections.Generic;
using Script.DataServices.Base;
using Script.Initializer;
using Script.Initializer.Base;
using Script.InputChecker.Base;
using Script.InputChecker.MouseKeyboard;
using Script.InteractableObject.Base;
using Script.InteractableObject.InteractableObjects.Container.Base;
using Script.InteractableObject.InteractableObjects.Container.Containers;
using Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Bed;
using Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Computer;
using Script.Libraries.MVVM;
using Script.Libraries.Observer.DataObserver;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Home.Initializer
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

    public void InitializeDependencies(IDataService dataService)
    {
        _dataService = dataService;
    }

    public IInitializable Initialize()
    {
        _observer = new DataObserver();
        
        InitializeInteractableObjects();
        
        InitializeInputControls();

        var interactableViewModelsContainer = new HomeInteractableViewModelsContainer(_viewModels.ToArray());
        
        return interactableViewModelsContainer;
    }

    private void InitializeInteractableObjects()
    {
        foreach (var interactableObject in _interactableObjects)
        {
            var interactableClickChecker = GetInteractableClickChecker(interactableObject.ClickTrackCollider);
            interactableObject.InitializeClickInput(interactableClickChecker);
            
            interactableObject.InitializeObserver(_observer);

            if (interactableObject is IView interactableView)
            {
                var viewModel = InitializeViewModelByViewAndGet(interactableView, _dataService);
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
        return new MouseClickChecker(_mainCamera, _inputControls.MouseKeyboard.Mouse, clickAreaCollider);
    }

    private void InitializeInputControls()
    {
        _inputControls = new InputControls();
        _inputControls.Enable();
        _mainCamera = Camera.main;
    }

    private IViewModel InitializeViewModelByViewAndGet(IView view, IDataService dataService)
    {
        switch (view)
        {
            case ComputerView:
                return new ComputerViewModel(view, dataService); 
            case BedView:
                break;
        }

        throw new Exception("");
    }
}
}