using System;
using System.Threading;
using System.Threading.Tasks;
using Script.ConfigData.LocationActionsConfig;
using Script.DataServices.Base;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.InteractableObject.ActionProgressSystem;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UiServiceProvider;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.UI.Dialogs.MainUI.MainHome;
using Script.UI.Dialogs.PopupDialogs.ActionsPopup;
using Script.Utils.ThreadUtils;
using UnityEngine;
using Random = System.Random;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Computer
{
public class ComputerViewModel : IViewModel
{
    private readonly ComputerModel _model;
    private readonly ComputerView _view;
    private readonly IUIServiceProvider _serviceProvider;
    private readonly InteractableObjectsConfig _interactableObjectsConfig;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;

    public ComputerViewModel(IView view,
        IDataService playerCharacteristicsService,
        IUIServiceProvider serviceProvider, 
        InteractableObjectsConfig interactableObjectsConfig,
        HomeActionProgressHandler homeActionProgressHandler)
    {
        if (view is not ComputerView computerView)
        {
            throw new Exception($"Need dependency of type {typeof(ComputerView)}");
        }

        _model = new ComputerModel();
        
        _view = computerView;
        
        _serviceProvider = serviceProvider;
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

        var viewModel = new ActionsUIViewModel(_serviceProvider, _interactableObjectsConfig.ComputerLocationActionData, _homeActionProgressHandler, _view.ProgressBarTransform.position); //передаю тупа 0 элемент списка, который за ПКАкшионс отвечает
        var popupsUIService = _serviceProvider.GetService<PopupsUIService>();
        popupsUIService.Show<ActionsUIView>(viewModel);
        
        var mainUIService = _serviceProvider.GetService<MainUIService>();
        
        var homeUIViewModel = mainUIService.CurrentUI as HomeUIViewModel;
        
        var applicationQuitTokenSource = new ApplicationQuitTokenSource();
        
        TestBubble(homeUIViewModel, applicationQuitTokenSource.Token);
        homeUIViewModel.ShowProgressBar(5, _view.ProgressBarTransform.position);
    }

    private async void TestBubble(HomeUIViewModel viewModel, CancellationToken token)
    {
        while (true)
        {
            var random = new Random();
            var next = random.Next(1, 5);
            viewModel.UpdateUpgradePoints(next, Vector3.zero);

            token.ThrowIfCancellationRequested();
            await Task.Delay(1000, token);
        }
    }

    #endregion
}
}