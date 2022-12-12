using System;
using Script.DataServices.Base;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.UI.Dialogs.PopupDialogs.ComputerActionsPopup;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Computer
{
public class ComputerViewModel : IViewModel
{
    private readonly ComputerModel _model;
    private readonly ComputerView _view;
    private readonly IUISystem _iuiSystem;

    public ComputerViewModel(IView view, IDataService playerCharacteristicsService, IUISystem iuiSystem)
    {
        if (view is not ComputerView computerView)
        {
            throw new Exception($"Need dependency of type {typeof(ComputerView)}");
        }

        _model = new ComputerModel();
        
        _view = computerView;
        _view.InitializeModel(_model);
        
        _iuiSystem = iuiSystem;

        SubscribeOnViewEvents();
        SubscribeOnModelEvents();

        _model.InputActive = true;
    }

    #region ViewEvents

    private void SubscribeOnViewEvents()
    {
        _view.ObjectClicked += OnViewObjectClicked;
    }

    private void SubscribeOnModelEvents()
    {
        _model.InputActiveChanged += OnInputActiveChanged;
    }

    private void OnInputActiveChanged(bool isActive)
    {
        _view.ChangeClickInputActive(isActive);
    }

    private void OnViewObjectClicked()
    {
        Debug.Log($"{_view.gameObject.name} was clicked");

        var viewModel = new ComputerActionsViewModel();
        _iuiSystem.Show(viewModel);
    }

    #endregion
}
}