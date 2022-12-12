using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.SceneSwitcherSystem.Container.Scenes;
using Script.SceneSwitcherSystem.Container.Scenes.Home;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Dialogs.FullscreenDialogs.ApplicationEnter;
using Script.UI.Dialogs.MainUI.MainHome;
using UnityEngine;

namespace Script.UI.Dialogs.FullscreenDialogs.StartGameMenu
{
public class StartGameMenuViewModel : UIViewModel
{
    private StartGameMenuEnterModel _model;
    private readonly IUISystem _uiSystem;
    private readonly ISceneSwitcher _sceneSwitcher;
    private StartGameView _view;

    public StartGameMenuViewModel(IUISystem uiSystem, ISceneSwitcher sceneSwitcher)
    {
        _uiSystem = uiSystem;
        _model = new StartGameMenuEnterModel();
        _sceneSwitcher = sceneSwitcher;
    }

    public override void ShowView()
    {
        _view = mainUiManager.Show<StartGameView>(this);
        _sceneSwitcher.SwitchTo<MainMenuScene>();
        
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
        var homeUIViewModel = new HomeUIViewModel(_sceneSwitcher);
        _uiSystem.Show(homeUIViewModel);
    }
}
}