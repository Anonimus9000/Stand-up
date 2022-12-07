using System.Collections.Generic;
using Script.Initializer;
using Script.Initializer.Base;
using Script.Libraries.Logger.LoggerBase;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Switcher;

namespace Script.UI.Manager
{
public class UIManagerMVVM : UIManager, IInitializable
{
    private readonly ISceneSwitcher _sceneSwitcher;

    public UIManagerMVVM(IInstantiater mainUIInstantiater,
        IInstantiater fullScreenUIInstantiater,
        IInstantiater popupsUIInstantiater,
        List<IUIWindow> windows,
        ILogger logger) : base(mainUIInstantiater, fullScreenUIInstantiater, popupsUIInstantiater, windows, logger)
    {
    }

    // public override T Show<T>()
    // {
    //     var uiWindow = base.Show<T>();
    // }
}
}