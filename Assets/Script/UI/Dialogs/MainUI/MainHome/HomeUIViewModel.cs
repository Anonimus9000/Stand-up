using Script.Libraries.UISystem.Managers.Instantiater;
using Script.SceneSwitcherSystem.Container.Scenes.Home;
using Script.UI.System;
using UnityEngine;

namespace Script.UI.Dialogs.MainUI.MainHome
{
public class HomeUIViewModel : UiViewModelBehaviour
{
    private HomeUIView _view;
    private readonly HomeUIModel _model;

    public HomeUIViewModel()
    {
        _model = new HomeUIModel();
    }

    public override void ShowView()
    {
        _view = mainUiManager.Show<HomeUIView>(this);
        sceneSwitcher.SwitchTo<HomeScene>();

        SubscribeOnModelEvents();
        SubscribeOnViewEvents();
    }

    public override void CloseView()
    {
        mainUiManager.TryCloseCurrent();

        UnsubscribeOnModelEvents();
        UnsubscribeOnViewEvents();
    }

    public override IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    public void UpdateUpgradePoints(int upgradePointsDifference, Vector3 startMovePosition)
    {
        _view.ShowMoveBubble(startMovePosition, upgradePointsDifference);
    }

    private void SubscribeOnModelEvents()
    {
        _model.UpgradePointsChanged += OnUpgradePointsChanged;
    }

    private void UnsubscribeOnModelEvents()
    {
        _model.UpgradePointsChanged -= OnUpgradePointsChanged;
    }

    private void SubscribeOnViewEvents()
    {
        _view.MoveBubbleCompleted += OnMoveBubbleCompleted;
    }

    private void UnsubscribeOnViewEvents()
    {
        _view.MoveBubbleCompleted -= OnMoveBubbleCompleted;
    }

    private void OnMoveBubbleCompleted(int upgradePointsCount)
    {
        _model.UpdateUpgradePoints(upgradePointsCount);
    }

    private void OnUpgradePointsChanged(int upgradePoints)
    {
    }
}
}