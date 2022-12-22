using System;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
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
    
    private StartGameMenuEnterModel _model;
    private StartGameMenuView _view;
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly IUIService _mainUIService;
    private readonly IUIService _fullScreenUIService;
    private readonly CharacterCreationData _characterData;
    private readonly CharacterSelector _characterSelector;
    private readonly PositionsConverter _positionsConverter;

    public StartGameMenuViewModel(
        IUIService mainUIService,
        IUIService fullScreenUIServiceISceneSwitcher,
        ISceneSwitcher sceneSwitcher,
        CharacterCreationData characterCreationData,
        CharacterSelector characterSelector,
        PositionsConverter positionsConverter)
    {
        _positionsConverter = positionsConverter;
        _characterData = characterCreationData;
        _sceneSwitcher = sceneSwitcher;
        _mainUIService = mainUIService;
        _fullScreenUIService = fullScreenUIServiceISceneSwitcher;
        _model = new StartGameMenuEnterModel();
        _characterSelector = characterSelector;
    }

    public void ShowView(IUIView view)
    {
        _sceneSwitcher.SwitchTo<MainMenuScene>();

        if (view is not StartGameMenuView startGameMenuView)
        {
            throw new Exception($"Incorrect type {view.GetType()}; Need {typeof(StartGameMenuView)}");
        }

        _view = startGameMenuView;
        
        _view.Show();
        
        ViewShown?.Invoke(this);
        
        SubscribeOnViewEvent(_view);
    }

    public void ShowHiddenView()
    {
        _view.Show();
        
        ViewHidden?.Invoke(this);
        
        SubscribeOnViewEvent(_view);
    }

    public void HideView()
    {
        _view.Hide();
        
        ViewHidden?.Invoke(this);
        
        UnsubscribeOnViewEvent(_view);
    }
    
    public IInstantiatable GetInstantiatable()
    {
        return _view;
    }

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

    private void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    private void OnCharacterCreationButtonPressed()
    {
        var characterCreationViewModel = new CharacterCreationViewModel(_sceneSwitcher, _fullScreenUIService, _characterData.CharacterList, _characterSelector);
        _fullScreenUIService.Show<CharacterCreationView>(characterCreationViewModel);
    }

    private void OnStartButtonPressed()
    {
        var homeUIViewModel = new HomeUIViewModel(
            _sceneSwitcher, 
            _mainUIService, 
            _fullScreenUIService, 
            _characterData,
            _characterSelector,
            _positionsConverter);
        
        _mainUIService.Show<HomeUIView>(homeUIViewModel);
    }
}
}