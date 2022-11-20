using System.Collections.Generic;
using Script.Initializer;
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
        List<IUIWindow> windows) : base(mainUIInstantiater, fullScreenUIInstantiater, popupsUIInstantiater, windows)
    {
    }

    public void Initialize()
    {
    }
}
}