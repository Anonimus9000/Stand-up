using System;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Container.Scenes.Home;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Converter;
using Script.UI.Dialogs.FullscreenDialogs.CharacterCreation.Components;
using Script.UI.Dialogs.FullscreenDialogs.CharacterInfo;
using Script.UI.Dialogs.MainUI.StartGameMenu;
using UnityEngine;

namespace Script.UI.Dialogs.MainUI.MainHome
{
public class HomeUIViewModel : IUIViewModel
{
    private HomeUIView _view;
    private readonly HomeUIModel _model;
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly IUIService _fullScreensUIService;
    private readonly IUIService _mainUiService;
    private readonly CharacterCreationData _characterData;
    private readonly CharacterSelector _characterSelector;
    private readonly PositionsConverter _positionsConverter;
    public event Action<IUIViewModel> ViewHidden;
    public event Action<IUIViewModel> ViewShown;

    public HomeUIViewModel(ISceneSwitcher sceneSwitcher, 
        IUIService mainUIService, 
        IUIService fullScreenService, 
        CharacterCreationData characterCreationData, 
        CharacterSelector characterSelector, 
        PositionsConverter positionsConverter)
    {
        _characterData = characterCreationData;
        _sceneSwitcher = sceneSwitcher;
        _mainUiService = mainUIService;
        _fullScreensUIService = fullScreenService;
        _positionsConverter = positionsConverter;
        _model = new HomeUIModel(_positionsConverter);
        _characterSelector = characterSelector;
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

    public void ShowProgressBar(float duration, Vector3 worldPosition)
    {
        var viewTransform = _view.transform as RectTransform;
        var progressBarPosition = _model.GetScreenPointPositionByWorld(worldPosition, viewTransform);
        _view.ShowProgressBar(duration, progressBarPosition);
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
        _view.ProgressCompleted += OnProgressBarCompleted;
    }

    private void UnsubscribeOnViewEvents()
    {
        _view.MoveBubbleCompleted -= OnMoveBubbleCompleted;
        _view.OpenCharacterInfoButtonPressed -= OnOpenCharacterInfoButtonPressed;
        _view.OpenStartGameMenuButtonPressed -= OnOpenStartGameMenuButtonPressed;
        _view.ViewShown -= OnViewShown;
        _view.ViewHidden -= OnViewHidden;
        _view.ProgressCompleted -= OnProgressBarCompleted;
    }

    private void OnProgressBarCompleted()
    {
        _view.CloseProgressBar();
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
        _mainUiService.Show<StartGameMenuView>(
            new StartGameMenuViewModel(
                _mainUiService, 
                _fullScreensUIService,
                _sceneSwitcher, 
                _characterData,
                _characterSelector,
                _positionsConverter));
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