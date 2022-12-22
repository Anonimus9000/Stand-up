using System;
using System.Threading;
using System.Threading.Tasks;
using Script.DataServices.Base;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.UI.Dialogs.MainUI.MainHome;
using Script.UI.Dialogs.PopupDialogs.ComputerActionsPopup;
using Script.Utils.ThreadUtils;
using UnityEngine;
using Random = System.Random;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Computer
{
public class ComputerViewModel : IViewModel
{
    private readonly ComputerModel _model;
    private readonly ComputerView _view;
    private readonly IUIService _popupsUIService;
    private readonly IUIService _mainUIService;

    public ComputerViewModel(IView view,
        IDataService playerCharacteristicsService,
        IUIService popupsUIService, 
        IUIService mainUIService)
    {
        if (view is not ComputerView computerView)
        {
            throw new Exception($"Need dependency of type {typeof(ComputerView)}");
        }

        _model = new ComputerModel();
        
        _view = computerView;
        
        _popupsUIService = popupsUIService;
        _mainUIService = mainUIService;

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

        var viewModel = new ComputerActionsIUIViewModel(_popupsUIService);
        _popupsUIService.Show<ComputerActionsUIView>(viewModel);
        
        var homeUIViewModel = _mainUIService.CurrentUI as HomeUIViewModel;
        
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