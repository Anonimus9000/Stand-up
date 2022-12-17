using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Container.Scenes;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Dialogs.FullscreenDialogs.ApplicationEnter;
using Script.UI.Dialogs.MainUI.MainHome;
using Script.UI.System;
using UnityEngine;

namespace Script.UI.Dialogs.FullscreenDialogs.StartGameMenu
{
public class StartGameMenuViewModel : UiViewModelBehaviour
{
    private StartGameMenuEnterModel _model;
    private readonly IUISystem _uiSystem;
    private StartGameView _view;

    public StartGameMenuViewModel(IUISystem uiSystem)
    {
        _uiSystem = uiSystem;
        _model = new StartGameMenuEnterModel();
    }

    public override void ShowView()
    {
        _view = mainUiManager.Show<StartGameView>(this);
        sceneSwitcher.SwitchTo<MainMenuScene>();
        
        SubscribeOnViewEvent(_view);
    }

    public override void CloseView()
    {
        mainUiManager.TryCloseCurrent();
    }

    public override IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    private void SubscribeOnViewEvent(StartGameView view)
    {
        view.OnStartPressed += OnStartButtonPressed;
        view.OnQuitPressed += OnQuitPressed;
    }

    private void OnQuitPressed()
    {
        Application.Quit();
    }

    private void OnStartButtonPressed()
    {
        var homeUIViewModel = new HomeUIViewModel();
        _uiSystem.Show(homeUIViewModel);
    }
}
}