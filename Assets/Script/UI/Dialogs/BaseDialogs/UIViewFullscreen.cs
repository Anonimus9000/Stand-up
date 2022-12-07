using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Switcher;

namespace Script.UI.Dialogs.BaseDialogs
{
public abstract class UIViewFullscreen : UIWindowViewBase, IFullScreen
{

    protected ISceneSwitcher sceneSwitcher;
    
    public void InitializeDependencies(ISceneSwitcher sceneSwitcher)
    {
        this.sceneSwitcher = sceneSwitcher;
    }
}
}