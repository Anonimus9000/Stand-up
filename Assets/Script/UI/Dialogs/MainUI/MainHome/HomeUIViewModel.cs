using System;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.InteractableObject.ActionProgressSystem;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.Service;
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
    private readonly HomeActionProgressHandler _homeActionProgressHandler;
    private IAnimatorService _animatorService;
    public event Action<IUIViewModel> ViewHidden;
    public event Action<IUIViewModel> ViewShown;

    public HomeUIViewModel(ISceneSwitcher sceneSwitcher, 
        IUIService mainUIService, 
        IUIService fullScreenService, 
        CharacterCreationData characterCreationData, 
        CharacterSelector characterSelector, 
        PositionsConverter positionsConverter,
        HomeActionProgressHandler homeActionProgressHandler)
    {
        _characterData = characterCreationData;
        _sceneSwitcher = sceneSwitcher;
        _mainUiService = mainUIService;
        _fullScreensUIService = fullScreenService;
        _positionsConverter = positionsConverter;
        _model = new HomeUIModel(_positionsConverter);
        _characterSelector = characterSelector;
        _homeActionProgressHandler = homeActionProgressHandler;
    }

    public void Init(IUIView view, IAnimatorService animatorService)
    {
        if (view is not HomeUIView homeUIView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(HomeUIView)}");
        }

        _view = homeUIView;
        _animatorService = animatorService;
        _sceneSwitcher.SwitchTo<HomeScene>();
        
        SubscribeOnModelEvents(_model);
        SubscribeOnViewEvents(_view);
        SubscribeOnAnimatorEvents(_animatorService);
    }

    public void Deinit()
    {
        UnsubscribeOnAnimatorEvents(_animatorService);
        UnsubscribeOnModelEvents(_model);
        UnsubscribeOnViewEvents(_view);
    }

    public void ShowView()
    {
        _animatorService.StartShowAnimation(_view);
    }

    public void ShowHiddenView()
    {
        _animatorService.StartShowAnimation(_view);
    }

    public void HideView()
    {
        _animatorService.StartHideAnimation(_view);
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

    #region ModelEvents

    private void SubscribeOnModelEvents(HomeUIModel model)
    {
        model.UpgradePointsChanged += OnUpgradePointsChanged;
    }

    private void UnsubscribeOnModelEvents(HomeUIModel model)
    {
        model.UpgradePointsChanged -= OnUpgradePointsChanged;
    }

    private void OnUpgradePointsChanged(int upgradePoints)
    {
    }

    #endregion

    #region ViewEvents

    private void SubscribeOnViewEvents(HomeUIView view)
    {
        view.MoveBubbleCompleted += OnMoveBubbleCompleted;
        view.OpenCharacterInfoButtonPressed += OnOpenCharacterInfoButtonPressed;
        view.OpenStartGameMenuButtonPressed += OnOpenStartGameMenuButtonPressed;
        view.ProgressCompleted += OnProgressBarCompleted;
    }

    private void UnsubscribeOnViewEvents(HomeUIView view)
    {
        view.MoveBubbleCompleted -= OnMoveBubbleCompleted;
        view.OpenCharacterInfoButtonPressed -= OnOpenCharacterInfoButtonPressed;
        view.OpenStartGameMenuButtonPressed -= OnOpenStartGameMenuButtonPressed;
        view.ProgressCompleted -= OnProgressBarCompleted;
    }
    
    private void OnProgressBarCompleted()
    {
        _view.CloseProgressBar();
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
                _positionsConverter,
                _homeActionProgressHandler));
    }
    
    private void OnOpenCharacterInfoButtonPressed()
    {
        _fullScreensUIService.Show<CharacterInfoView>(new CharacterInfoViewModel(_fullScreensUIService));
    }

    private void OnMoveBubbleCompleted(int upgradePointsCount)
    {
        _model.UpdateUpgradePoints(upgradePointsCount);
    }

    #endregion

    #region AnimatorEvents

    private void SubscribeOnAnimatorEvents(IAnimatorService animatorService)
    {
        animatorService.ShowCompleted += OnShowAnimationCompleted;
        animatorService.HideCompleted += OnHideAnimationCompleted;
    }

    private void UnsubscribeOnAnimatorEvents(IAnimatorService animatorService)
    {
        animatorService.ShowCompleted -= OnShowAnimationCompleted;
        animatorService.HideCompleted -= OnHideAnimationCompleted;
    }

    private void OnShowAnimationCompleted()
    {
        _view.OnShown();
        ViewShown?.Invoke(this);
    }

    private void OnHideAnimationCompleted()
    {
        _view.OnHidden();
        ViewHidden?.Invoke(this);
    }

    #endregion

}
}