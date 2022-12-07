using System.Collections.Generic;
using Script.Initializer;
using Script.Initializer.Base;
using Script.Libraries.Logger.LoggerBase;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;

namespace Script.UI.Manager
{
public class UIManagerInitializable : UIManager, IInitializable
{
    public UIManagerInitializable(IInstantiater mainUIInstantiater,
        IInstantiater fullScreenUIInstantiater,
        IInstantiater popupsUIInstantiater,
        List<IUIWindow> windows, ILogger logger) : base(mainUIInstantiater, fullScreenUIInstantiater, popupsUIInstantiater, windows, logger)
    {
    }

    public void Initialize()
    {
    }
}
}