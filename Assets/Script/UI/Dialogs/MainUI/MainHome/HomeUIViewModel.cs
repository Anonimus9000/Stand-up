using Script.Libraries.UISystem.Managers.Instantiater;
using Script.SceneSwitcherSystem.Container.Scenes.Home;
using Script.UI.System;

namespace Script.UI.Dialogs.MainUI.MainHome
{
public class HomeUIViewModel : UiViewModelBehaviour
{
    private HomeUIView _view;
    private readonly HomeUIModel _model;

    public HomeUIViewModel()
    {
        _model = new HomeUIModel();
    }
    
    public override void ShowView()
    {
        _view = mainUiManager.Show<HomeUIView>(this);
        sceneSwitcher.SwitchTo<HomeScene>();
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