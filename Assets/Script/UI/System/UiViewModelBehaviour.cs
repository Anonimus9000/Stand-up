using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UiMVVM;
using Script.SceneSwitcherSystem.Switcher;

namespace Script.UI.System
{
public abstract class UiViewModelBehaviour : UIViewModel
{
    protected ISceneSwitcher sceneSwitcher;
    public abstract override void ShowView();

    public abstract override void CloseView();

    public abstract override IInstantiatable GetInstantiatable();

    public void InitializeDependency(ISceneSwitcher sceneSwitcher)
    {
        this.sceneSwitcher = sceneSwitcher;
    }
}
}