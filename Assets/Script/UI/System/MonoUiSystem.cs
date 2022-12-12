using System.Collections.Generic;
using Script.Initializer;
using Script.Libraries.Logger.LoggerBase;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Switcher;

namespace Script.UI.System
{
public class MonoUiSystem : UISystem, IInitializable
{
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly ILogger _logger;

    public MonoUiSystem(IInstantiater mainUIInstantiater,
        IInstantiater fullScreenUIInstantiater,
        IInstantiater popupsUIInstantiater,
        List<IUIView> windows,
        ILogger logger) : base(mainUIInstantiater, fullScreenUIInstantiater, popupsUIInstantiater, windows)
    {
        _logger = logger;
    }

    public override UIViewModel Show(UIViewModel viewModel)
    {
        _logger.Log($"Show {viewModel.GetType()}");
        
        return base.Show(viewModel);
    }

    public override void Close(UIViewModel viewModel)
    {
        _logger.Log($"Close {viewModel.GetType()}");
        
        base.Close(viewModel);
    }
}
}