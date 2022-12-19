using System;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Container.Scenes;
using Script.SceneSwitcherSystem.Switcher;
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

    public StartGameMenuViewModel(
        IUIService mainUIService,
        IUIService fullScreenUIServiceISceneSwitcher,
        ISceneSwitcher sceneSwitcher)
    {
        _sceneSwitcher = sceneSwitcher;
        _mainUIService = mainUIService;
        _fullScreenUIService = fullScreenUIServiceISceneSwitcher;
        _model = new StartGameMenuEnterModel();
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
    }

    public void HideView()
    {
        _view.Hide();
        
        ViewHidden?.Invoke(this);
    }
    
    public IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    private void SubscribeOnViewEvent(StartGameMenuView menuView)
    {
        menuView.OnStartPressed += OnStartButtonPressed;
        menuView.OnQuitPressed += OnQuitPressed;
    }

    private void OnQuitPressed()
    {
        Application.Quit();
    }

    private void OnStartButtonPressed()
    {
        var homeUIViewModel = new HomeIUIViewModel(_sceneSwitcher, _mainUIService, _fullScreenUIService);
        _mainUIService.Show<HomeUIView>(homeUIViewModel);
    }
}
}