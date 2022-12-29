using System;
using Script.DataServices.Base;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.UI.Dialogs.PopupDialogs.ActionsPopup;
using Script.UI.Dialogs.PopupDialogs.InteractableObjectsData;
using Script.UI.Dialogs.PopupDialogs.ToiletActionsPopup;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Toilet
{
public class ToiletViewModel: IViewModel
{
    private readonly ToiletModel _model;
    private readonly ToiletView _view;
    private readonly IUIServiceProvider _uiServiceProvider;
    private readonly InteractableObjectsData _interactableObjectsData;
    private readonly ActionProgressHandler _actionProgressHandler;

    public ToiletViewModel(IView view, 
        IDataService playerCharacteristicsService, 
        IUIServiceProvider uiServiceProvider,
        InteractableObjectsData interactableObjectsData,
        ActionProgressHandler actionProgressHandler)
    {
        if (view is not ToiletView toiletView)
        {
            throw new Exception($"Need dependency of type {typeof(ToiletView)}");
        }

        _model = new ToiletModel();
        
        _view = toiletView;
        
        _uiServiceProvider = uiServiceProvider;
        
        _interactableObjectsData = interactableObjectsData;
        _actionProgressHandler = actionProgressHandler;

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

        var popupsUIService = _uiServiceProvider.GetService<PopupsUIService>();
        var viewModel = new ActionsUIViewModel(_uiServiceProvider, _interactableObjectsData.InteractableObjects[1], _actionProgressHandler, _view.ProgressBarPosition.position);
        
        popupsUIService.CloseAll();
        popupsUIService.Show<ActionsUIView>(viewModel);
    }

    #endregion
}
}