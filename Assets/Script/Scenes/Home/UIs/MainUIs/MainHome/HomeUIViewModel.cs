﻿using System;
using Script.DataServices.Base;
using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.SceneSwitcherSystem;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;
using Script.Scenes.Home.ActionProgressSystem.Handler;
using Script.Scenes.Home.UIs.MainUIs.MainHome.Components;
using Script.Scenes.MainMenu.UIs.FullScreens.CharacterCreation.Components;
using Script.Scenes.MainMenu.UIs.MainUIs.StartGameMenu;
using Script.UI.CommonUIs.FullscreenDialogs.CharacterInfo;
using Script.Utils.UIPositionConverter;
using UnityEngine;

namespace Script.Scenes.Home.UIs.MainUIs.MainHome
{
public class HomeUIViewModel : UIViewModel
{
    private HomeUIView _view;
    private readonly HomeUIModel _model;
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly ICharacterModelsConfig _characterModelsConfig;
    private readonly CharacterSelector _characterSelector;
    private readonly PositionsConverter _positionsConverter;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;
    private IAnimatorService _animatorService;
    private ProgressBar _currentProgressBar;
    private readonly IDataService _playerData;
    private readonly IResourceLoader _resourceLoader;
    private readonly IUIServiceProvider _uiServiceProvider;
    public override event Action<IUIViewModel> ViewHidden;
    public override UIType UIType { get; }
    public override event Action<IUIViewModel> ViewShown;

    public HomeUIViewModel(ISceneSwitcher sceneSwitcher,
        IUIServiceProvider uiServiceProvider,
        ICharacterModelsConfig characterCreationModelsConfig,
        CharacterSelector characterSelector,
        PositionsConverter positionsConverter,
        HomeActionProgressHandler homeActionProgressHandler,
        IDataService playerData,
        IResourceLoader resourceLoader,
        Camera mainCamera, 
        Canvas canvas)
    {
        UIType = UIType.Main;
        
        _playerData = playerData;

        _characterModelsConfig = characterCreationModelsConfig;
        _sceneSwitcher = sceneSwitcher;
        _uiServiceProvider = uiServiceProvider;
        _positionsConverter = positionsConverter;
        _characterSelector = characterSelector;
        _homeActionProgressHandler = homeActionProgressHandler;
        _resourceLoader = resourceLoader;
        
        _model = AddDisposable(new HomeUIModel(positionsConverter, _uiServiceProvider, homeActionProgressHandler, canvas, mainCamera));
        AddDisposable(_model);
    }

    public override void Init(IUIView view, IAnimatorService animatorService)
    {
        if (view is not HomeUIView homeUIView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(HomeUIView)}");
        }

        _view = AddDisposable(homeUIView);
        _animatorService = animatorService;
        
        SubscribeOnModelEvents(_model);
        _model.InitUpgradePoints(0);
        
        SubscribeOnViewEvents(_view);
        SubscribeOnAnimatorEvents(_animatorService);
    }

    public override void Deinit()
    {
        UnsubscribeOnAnimatorEvents(_animatorService);
        UnsubscribeOnModelEvents(_model);
        UnsubscribeOnViewEvents(_view);
    }

    public override void ShowView()
    {
        _animatorService.StartShowAnimation(_view);
    }

    public override void ShowHiddenView()
    {
        _animatorService.StartShowAnimation(_view);
    }

    public override void HideView()
    {
        _animatorService.StartHideAnimation(_view);
    }

    public override IInstantiatable GetInstantiatable()
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
        _uiServiceProvider.Show<StartGameMenuView>(
            AddDisposable(new StartGameMenuViewModel(
                _uiServiceProvider,
                _sceneSwitcher, 
                _characterModelsConfig,
                _positionsConverter,
                _playerData,
                _resourceLoader)));
    }
    
    private void OnOpenCharacterInfoButtonPressed()
    {
        _uiServiceProvider.Show<CharacterInfoView>(AddDisposable(new CharacterInfoViewModel(_uiServiceProvider, _playerData)));
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