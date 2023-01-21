using System;
using Script.InteractableObject.ActionProgressSystem;
using Script.InteractableObject.ActionProgressSystem.Handler;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Container.Scenes;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Converter;
using Script.UI.Dialogs.FullscreenDialogs.CharacterCreation;
using Script.UI.Dialogs.FullscreenDialogs.CharacterCreation.Components;
using Script.UI.Dialogs.MainUI.MainHome;
using UnityEngine;

namespace Script.UI.Dialogs.MainUI.StartGameMenu
{
public class StartGameMenuViewModel : IUIViewModel
{
    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;
    
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly IUIService _mainUIService;
    private readonly IUIService _fullScreenUIService;
    private readonly IUIService _popupsUIService;
    private readonly CharacterCreationData _characterData;
    private readonly CharacterSelector _characterSelector;
    private readonly PositionsConverter _positionsConverter;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;

    private StartGameMenuEnterModel _model;
    private StartGameMenuView _view;
    private IAnimatorService _animatorService;

    public StartGameMenuViewModel(
        IUIService mainUIService,
        IUIService fullScreenUIServiceISceneSwitcher,
        IUIService popupsUIService,
        ISceneSwitcher sceneSwitcher,
        CharacterCreationData characterCreationData,
        CharacterSelector characterSelector,
        PositionsConverter positionsConverter,
        HomeActionProgressHandler homeActionProgressHandler)
    {
        _positionsConverter = positionsConverter;
        _characterData = characterCreationData;
        _sceneSwitcher = sceneSwitcher;
        _mainUIService = mainUIService;
        _popupsUIService = popupsUIService;
        _fullScreenUIService = fullScreenUIServiceISceneSwitcher;
        _model = new StartGameMenuEnterModel();
        _characterSelector = characterSelector;
        _homeActionProgressHandler = homeActionProgressHandler;
    }

    public void Init(IUIView view, IAnimatorService animatorService)
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

    public void Deinit()
    {
        UnsubscribeOnViewEvent(_view);
        UnsubscribeOnAnimatorEvents(_animatorService);
    }

    public void ShowView()
    {
        _sceneSwitcher.SwitchTo<MainMenuScene>();

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
        var characterCreationViewModel = new CharacterCreationViewModel(_sceneSwitcher, _fullScreenUIService,
            _characterData.CharacterList, _characterSelector);
        _fullScreenUIService.Show<CharacterCreationView>(characterCreationViewModel);
    }

    private void OnStartButtonPressed()
    {
        var homeUIViewModel = new HomeUIViewModel(
            _sceneSwitcher,
            _mainUIService,
            _fullScreenUIService,
            _popupsUIService,
            _characterData,
            _characterSelector,
            _positionsConverter,
            _homeActionProgressHandler);

        _mainUIService.Show<HomeUIView>(homeUIViewModel);
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