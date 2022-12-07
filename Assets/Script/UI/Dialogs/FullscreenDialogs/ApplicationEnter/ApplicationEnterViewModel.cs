using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Container.Scenes.Home;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Dialogs.MainUI;
using UnityEngine;

namespace Script.UI.Dialogs.FullscreenDialogs.ApplicationEnter
{
public class ApplicationEnterViewModel : IViewModel
{
    private ApplicationEnterModel _model;
    private readonly IUIManager _uiManager;
    private readonly ISceneSwitcher _sceneSwitcher;

    public ApplicationEnterViewModel(IView view, IModel model, IUIManager uiManager, ISceneSwitcher sceneSwitcher)
    {
        var applicationEnterView = (ApplicationEnterView) view;
        _model = (ApplicationEnterModel) model;
        _uiManager = uiManager;
        _sceneSwitcher = sceneSwitcher;
        
        SubscribeOnViewEvent(applicationEnterView);
    }

    private void SubscribeOnViewEvent(ApplicationEnterView view)
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
        _uiManager.Show<MainHomeUI>();
        _sceneSwitcher.SwitchTo<HomeScene>();
    }
    
}
}