using System.Collections.Generic;
using Script.Initializer;
using Script.Libraries.Logger.LoggerBase;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Switcher;

namespace Script.UI.System
{
public class UiServiceProviderInitializable : UIServiceProvider, IInitializable
{
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly ILogger _logger;

    public UiServiceProviderInitializable(IInstantiater mainUIInstantiater,
        IInstantiater fullScreenUIInstantiater,
        IInstantiater popupsUIInstantiater,
        List<IUIView> windows,
        ILogger logger,
        ISceneSwitcher sceneSwitcher) : base(mainUIInstantiater, fullScreenUIInstantiater, popupsUIInstantiater, windows)
    {
        _sceneSwitcher = sceneSwitcher;
        _logger = logger;
    }
}
}