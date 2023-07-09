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
using Script.Scenes.Home;
using Script.Scenes.MainMenu.UIs.FullScreens.CharacterCreation;
using Script.Utils.UIPositionConverter;
using UnityEngine;

namespace Script.Scenes.MainMenu.UIs.MainUIs.StartGameMenu
{
public class StartGameMenuViewModel : UIViewModel
{
    public override event Action<IUIViewModel> ViewShown;
    public override event Action<IUIViewModel> ViewHidden;
    
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly IUIService _mainUIService;
    private readonly IUIService _fullScreenUIService;
    private readonly IUIService _popupsUIService;
    private readonly ICharacterModelsConfig _characterData;
    private readonly PositionsConverter _positionsConverter;

    private StartGameMenuEnterModel _model;
    private StartGameMenuView _view;
    private IAnimatorService _animatorService;
    private readonly IDataService _playerDataService;
    private IResourceLoader _resourceLoader;

    public StartGameMenuViewModel(
        IUIServiceLocator uiServiceLocator,
        ISceneSwitcher sceneSwitcher,
        ICharacterModelsConfig characterConfig,
        PositionsConverter positionsConverter,
        IDataService playerDataService,
        IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _playerDataService = playerDataService;
        _positionsConverter = positionsConverter;
        _characterData = characterConfig;
        _sceneSwitcher = sceneSwitcher;
        _mainUIService = uiServiceLocator.GetService<MainUIService>();
        _popupsUIService = uiServiceLocator.GetService<PopupsUIService>();
        _fullScreenUIService = uiServiceLocator.GetService<FullScreensUIService>();
        _model = AddDisposable(new StartGameMenuEnterModel());
    }

    public override void Init(IUIView view, IAnimatorService animatorService)
    {
        if (view is not StartGameMenuView startGameMenuView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(StartGameMenuView)}");
        }

        _view = startGameMenuView;
        _animatorService = animatorService;

        SubscribeOnAnimatorEvents(_animatorService);
        SubscribeOnViewEvent(_view);
        ;
    }

    public override void Deinit()
    {
        UnsubscribeOnViewEvent(_view);
        UnsubscribeOnAnimatorEvents(_animatorService);
    }

    public override void ShowView()
    {
        _sceneSwitcher.SwitchTo<MainMenuScene>();

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

    #region ModelEvents

    #endregion

    #region ViewEvents

    private void SubscribeOnViewEvent(StartGameMenuView menuView)
    {
        menuView.StartPressed += OnStartButtonPressed;
        menuView.QuitPressed += OnQuitButtonPressed;
        menuView.CharacterCreationButtonPressed += OnCharacterCreationButtonPressed;
    }

    private void UnsubscribeOnViewEvent(StartGameMenuView menuView)
    {
        menuView.StartPressed -= OnStartButtonPressed;
        menuView.QuitPressed -= OnQuitButtonPressed;
        menuView.CharacterCreationButtonPressed -= OnCharacterCreationButtonPressed;
    }

    private void OnCharacterCreationButtonPressed()
    {
        var characterCreationViewModel = AddDisposable(new CharacterCreationViewModel(
            _resourceLoader, 
            _sceneSwitcher,
            _fullScreenUIService,
            _characterData.CharacterModels));
        
        _fullScreenUIService.Show<CharacterCreationView>(characterCreationViewModel);
    }

    private void OnStartButtonPressed()
    {
        _sceneSwitcher.SwitchTo<HomeScene>();
    }

    private void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    #endregion

    #region AnimatorEvents

    private void SubscribeOnAnimatorEvents(IAnimatorService animatorService)
    {
        animatorService.ShowCompleted += OnAnimationShowCompleted;
        animatorService.HideCompleted += OnAnimationHideCompleted;
    }

    private void UnsubscribeOnAnimatorEvents(IAnimatorService animatorService)
    {
        animatorService.ShowCompleted -= OnAnimationShowCompleted;
        animatorService.HideCompleted -= OnAnimationHideCompleted;
    }

    private void OnAnimationShowCompleted()
    {
        _view.OnShown();

        ViewShown?.Invoke(this);
    }

    private void OnAnimationHideCompleted()
    {
        _view.OnHidden();

        ViewHidden?.Invoke(this);
    }

    #endregion
}
}