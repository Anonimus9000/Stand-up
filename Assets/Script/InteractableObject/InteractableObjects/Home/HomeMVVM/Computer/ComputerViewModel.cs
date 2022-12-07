using System;
using Script.DataServices.Base;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.UI.Dialogs.PopupDialogs;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Computer
{
public class ComputerViewModel : IViewModel
{
    private readonly ComputerModel _model;
    private readonly ComputerView _view;
    private readonly IUIManager _uiManager;

    public ComputerViewModel(IView view, IDataService playerCharacteristicsService, IUIManager uiManager)
    {
        if (view is not ComputerView computerView)
        {
            throw new Exception($"Need dependency of type {typeof(ComputerView)}");
        }

        _model = new ComputerModel();
        
        _view = computerView;
        _view.Initialize(_model);
        
        _uiManager = uiManager;

        SubscribeOnViewEvents();
    }

    #region ViewEvents

    private void SubscribeOnViewEvents()
    {
        _view.ObjectClicked += OnViewObjectClicked;
    }

    private void OnViewObjectClicked()
    {
        Debug.Log($"{_view.gameObject.name} was clicked");

        _uiManager.Show<PCActionPopup>();
    }

    #endregion
}
}