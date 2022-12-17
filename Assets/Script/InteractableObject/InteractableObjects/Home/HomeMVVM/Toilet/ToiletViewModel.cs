using System;
using Script.DataServices.Base;
using Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Computer;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.UI.Dialogs.PopupDialogs.ComputerActionsPopup;
using Script.UI.Dialogs.PopupDialogs.ToiletActionsPopup;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Toilet
{
public class ToiletViewModel: IViewModel
{
    private readonly ToiletModel _model;
    private readonly ToiletView _view;
    private readonly IUISystem _iuiSystem;

    public ToiletViewModel(IView view, IDataService playerCharacteristicsService, IUISystem iuiSystem)
    {
        if (view is not ToiletView toiletView)
        {
            throw new Exception($"Need dependency of type {typeof(ToiletView)}");
        }

        _model = new ToiletModel();
        
        _view = toiletView;
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

        var viewModel = new ToiletUIActionsViewModel();
        _iuiSystem.CloseAllPopups();
        _iuiSystem.Show(viewModel);
    }

    #endregion
}
}