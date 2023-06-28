using System;
using Script.DataServices.Base;
using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.SceneSwitcherSystem;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;
using Script.Scenes.Common.ActionProgressSystem.Handler;
using Script.Scenes.Home.ActionProgressSystem.Handler;
using Script.Scenes.Home.UIs.MainUIs.MainHome.Components;
using Script.Scenes.MainMenu.UIs.FullScreens.CharacterCreation.Components;
using Script.Scenes.MainMenu.UIs.MainUIs.StartGameMenu;
using Script.UI.CommonUIs.FullscreenDialogs.CharacterInfo;
using Script.Utils.UIPositionConverter;
using UnityEngine;

namespace Script.Scenes.Home.UIs.MainUIs.MainHome
{
public class HomeUIViewModel : IUIViewModel
{
    private HomeUIView _view;
    private readonly HomeUIModel _model;
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly IUIService _fullScreensUIService;
    private readonly IUIService _mainUiService;
    private readonly ICharacterModelsConfig _characterModelsConfig;
    private readonly CharacterSelector _characterSelector;
    private readonly PositionsConverter _positionsConverter;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;
    private IAnimatorService _animatorService;
    private ProgressBar _currentProgressBar;
    private readonly IUIService _popupsUIService;
    private readonly IDataService _playerData;
    private IResourceLoader _resourceLoader;
    private IUIServiceLocator _uiServiceLocator;
    public event Action<IUIViewModel> ViewHidden;
    public event Action<IUIViewModel> ViewShown;

    public HomeUIViewModel(
        ISceneSwitcher sceneSwitcher, 
        IUIServiceLocator uiServiceLocator,
        ICharacterModelsConfig characterCreationModelsConfig, 
        CharacterSelector characterSelector, 
        PositionsConverter positionsConverter,
        HomeActionProgressHandler homeActionProgressHandler,
        IDataService playerData,
        IResourceLoader resourceLoader)
    {
        _playerData = playerData;

        _characterModelsConfig = characterCreationModelsConfig;
        _sceneSwitcher = sceneSwitcher;
        _mainUiService = uiServiceLocator.GetService<MainUIService>();
        _fullScreensUIService = uiServiceLocator.GetService<FullScreensUIService>();
        _popupsUIService = uiServiceLocator.GetService<PopupsUIService>();
        _positionsConverter = positionsConverter;
        _characterSelector = characterSelector;
        _homeActionProgressHandler = homeActionProgressHandler;
        _uiServiceLocator = uiServiceLocator;
        _resourceLoader = resourceLoader;
        
        _model = new HomeUIModel(positionsConverter, _popupsUIService, homeActionProgressHandler);
    }

    public void Init(IUIView view, IAnimatorService animatorService)
    {
        if (view is not HomeUIView homeUIView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(HomeUIView)}");
        }

        _view = homeUIView;
        _animatorService = animatorService;
        
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

    public void UpdateStress(float stressPoint)
    {
        _view.UpdateStress(stressPoint);
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
            _view.UpgradePointsIcon,
            _homeActionProgressHandler);

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
                _uiServiceLocator,
                _sceneSwitcher, 
                _characterModelsConfig,
                _positionsConverter,
                _playerData,
                _resourceLoader));
    }
    
    private void OnOpenCharacterInfoButtonPressed()
    {
        _fullScreensUIService.Show<CharacterInfoView>(new CharacterInfoViewModel(_fullScreensUIService, _playerData));
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