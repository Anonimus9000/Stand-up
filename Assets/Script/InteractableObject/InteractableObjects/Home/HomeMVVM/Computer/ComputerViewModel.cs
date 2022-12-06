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
    public ComputerModel Model;

    public ComputerView View;
    private readonly IUIManager _uiManager;

    public ComputerViewModel(IView view, IDataService playerCharacteristicsService, IUIManager uiManager)
    {
        if (view is not ComputerView computerView)
        {
            throw new Exception($"Need dependency of type {typeof(ComputerView)}");
        }

        View = computerView;
        View.Initialize(this);
        Model = new ComputerModel();
        _uiManager = uiManager;

        SubscribeOnViewEvents();
    }

    private void SubscribeOnViewEvents()
    {
        View.ObjectClicked += OnViewObjectClicked;
    }

    private void OnViewObjectClicked()
    {
        Debug.Log($"{View.gameObject.name} was clicked");

        _uiManager.Show<PCActionPopup>();
    }
}
}