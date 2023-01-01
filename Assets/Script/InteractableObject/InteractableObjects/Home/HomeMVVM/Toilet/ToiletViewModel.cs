using System;
using Script.ConfigData.LocationActionsConfig;
using Script.DataServices.Base;
using Script.InteractableObject.ActionProgressSystem;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UiServiceProvider;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.UI.Dialogs.PopupDialogs.ActionsPopup;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Toilet
{
public class ToiletViewModel: IViewModel
{
    private readonly ToiletModel _model;
    private readonly ToiletView _view;
    private readonly IUIServiceProvider _uiServiceProvider;
    private readonly InteractableObjectsConfig _interactableObjectsConfig;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;

    public ToiletViewModel(IView view, 
        IDataService playerCharacteristicsService, 
        IUIServiceProvider uiServiceProvider,
        InteractableObjectsConfig interactableObjectsConfig,
        HomeActionProgressHandler homeActionProgressHandler)
    {
        if (view is not ToiletView toiletView)
        {
            throw new Exception($"Need dependency of type {typeof(ToiletView)}");
        }

        _model = new ToiletModel();
        
        _view = toiletView;
        
        _uiServiceProvider = uiServiceProvider;
        
        _interactableObjectsConfig = interactableObjectsConfig;
        _homeActionProgressHandler = homeActionProgressHandler;

        SubscribeOnViewEvents();
        SubscribeOnModelEvents();

        _model.InputActive = true;
    }

    #region ModelEvents

    private void SubscribeOnModelEvents()
    {
        _model.InputActiveChanged += OnInputActiveChanged;
    }

    private void OnInputActiveChanged(bool isActive)
    {
        _view.ChangeClickInputActive(isActive);
    }

    #endregion

    #region ViewEvents

    private void SubscribeOnViewEvents()
    {
        _view.ObjectClicked += OnViewObjectClicked;
    }

    private void OnViewObjectClicked()
    {
        Debug.Log($"{_view.gameObject.name} was clicked");

        var popupsUIService = _uiServiceProvider.GetService<PopupsUIService>();
        var viewModel = new ActionsUIViewModel(_uiServiceProvider, _interactableObjectsConfig.ToiletLocationActionData,
            _homeActionProgressHandler, _view.ProgressBarPosition.position);
        
        popupsUIService.CloseAll();
        popupsUIService.Show<ActionsUIView>(viewModel);
    }

    #endregion
}
}