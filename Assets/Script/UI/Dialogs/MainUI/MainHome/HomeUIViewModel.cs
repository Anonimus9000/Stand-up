using System;
using Script.InteractableObject.ActionProgressSystem.Handler;
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
using Script.UI.Dialogs.MainUI.MainHome.Components;
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
    private ProgressBar _currentProgressBar;
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
        _model.InitUpgradePoints(0);
        
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
        
    }

    public ProgressBar ShowProgressBar(float duration, Vector3 worldPosition, int upgradePoints)
    {
        _currentProgressBar = _model.ShowProgress(
            upgradePoints,
            duration,
            worldPosition, 
            _view.ProgressBarPrefab, 
            _view.ProgressBarParen,
            _view.FlyBubblePrefab,
            _view.BubbleParent,
            _view.UpgradePointsIcon);

        return _currentProgressBar;
    }

    #region ModelEvents

    private void SubscribeOnModelEvents(HomeUIModel model)
    {
        model.UpgradePoints.Subscribe(OnUpgradePointsChanged);
    }

    private void UnsubscribeOnModelEvents(HomeUIModel model)
    {
        model.UpgradePoints.Unsubscribe(OnUpgradePointsChanged);
    }

    private void OnUpgradePointsChanged(int upgradePoints)
    {
        _view.UpgradePoints.text = upgradePoints.ToString();
    }

    #endregion

    #region ViewEvents

    private void SubscribeOnViewEvents(HomeUIView view)
    {
        view.OpenCharacterInfoButtonPressed += OnOpenCharacterInfoButtonPressed;
        view.OpenStartGameMenuButtonPressed += OnOpenStartGameMenuButtonPressed;
    }

    private void UnsubscribeOnViewEvents(HomeUIView view)
    {
        view.OpenCharacterInfoButtonPressed -= OnOpenCharacterInfoButtonPressed;
        view.OpenStartGameMenuButtonPressed -= OnOpenStartGameMenuButtonPressed;
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