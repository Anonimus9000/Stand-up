using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UiMVVM;
using Script.SceneSwitcherSystem.Container.Scenes.Home;
using Script.SceneSwitcherSystem.Switcher;

namespace Script.UI.Dialogs.MainUI.MainHome
{
public class HomeUIViewModel : UIViewModel
{
    private HomeUIView _view;
    private readonly HomeUIModel _model;
    private readonly ISceneSwitcher _sceneSwitcher;

    public HomeUIViewModel(ISceneSwitcher sceneSwitcher)
    {
        _model = new HomeUIModel();
        _sceneSwitcher = sceneSwitcher;
    }
    
    public override void ShowView()
    {
        _view = mainUiManager.Show<HomeUIView>(this);
        _sceneSwitcher.SwitchTo<HomeScene>();
    }

    public override void CloseView()
    {
        mainUiManager.TryCloseCurrent();
    }

    public override IInstantiatable GetInstantiatable()
    {
        return _view;
    }
}
}