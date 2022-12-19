using System;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Container.Scenes.Home;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Dialogs.FullscreenDialogs.CharacterInfo;
using Script.UI.Dialogs.MainUI.StartGameMenu;
using UnityEngine;

namespace Script.UI.Dialogs.MainUI.MainHome
{
public class HomeIUIViewModel : IUIViewModel
{
    private HomeUIView _view;
    private readonly HomeUIModel _model;
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly IUIService _fullScreensUIService;
    private readonly IUIService _mainUiService;
    public event Action<IUIViewModel> ViewHidden;
    public event Action<IUIViewModel> ViewShown;

    public HomeIUIViewModel(ISceneSwitcher sceneSwitcher, IUIService mainUIService, IUIService fullScreenService)
    {
        _sceneSwitcher = sceneSwitcher;
        _mainUiService = mainUIService;
        _fullScreensUIService = fullScreenService;
        _model = new HomeUIModel();
    }

    public void ShowView(IUIView view)
    {
        _sceneSwitcher.SwitchTo<HomeScene>();

        if (view is not HomeUIView homeUIView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(HomeUIView)}");
        }

        _view = homeUIView;
        _view.Show();
        ViewShown?.Invoke(this);

        SubscribeOnModelEvents();
        SubscribeOnViewEvents();
    }

    public void ShowHiddenView()
    {
        _view.Show();
        ViewShown?.Invoke(this);
        
        SubscribeOnModelEvents();
        SubscribeOnViewEvents();
    }

    public void HideView()
    {
        _view.Hide();
        ViewHidden?.Invoke(this);
        
        UnsubscribeOnModelEvents();
        UnsubscribeOnViewEvents();
    }

    public IInstantiatable GetInstantiatable()
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
        _view.OpenCharacterInfoButtonPressed += OnOpenCharacterInfoButtonPressed;
        _view.OpenStartGameMenuButtonPressed += OnOpenStartGameMenuButtonPressed;
        _view.ViewShown += OnViewShown;
        _view.ViewHidden += OnViewHidden;
    }

    private void UnsubscribeOnViewEvents()
    {
        _view.MoveBubbleCompleted -= OnMoveBubbleCompleted;
        _view.OpenCharacterInfoButtonPressed -= OnOpenCharacterInfoButtonPressed;
        _view.OpenStartGameMenuButtonPressed -= OnOpenStartGameMenuButtonPressed;
        _view.ViewShown -= OnViewShown;
        _view.ViewHidden -= OnViewHidden;
    }

    private void OnViewShown()
    {
        ViewShown?.Invoke(this);
    }

    private void OnViewHidden()
    {
        ViewHidden?.Invoke(this);
    }

    private void OnOpenStartGameMenuButtonPressed()
    {
        _mainUiService.Show<StartGameMenuView>(new StartGameMenuViewModel(_mainUiService, _fullScreensUIService, _sceneSwitcher));
    }

    private void OnOpenCharacterInfoButtonPressed()
    {
        _fullScreensUIService.Show<CharacterInfoView>(new CharacterInfoViewModel(_fullScreensUIService));
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