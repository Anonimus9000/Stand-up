using System.Collections.Generic;
using Script.Initializer;
using Script.ProjectLibraries.Logger.LoggerBase;
using Script.ProjectLibraries.SceneSwitcherSystem;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.UI.System
{
public class UIServiceLocatorInitializable : UIServiceLocator
{
    private readonly ISceneSwitcher _sceneSwitcher;
    private readonly ILogger _logger;

    public UIServiceLocatorInitializable(IInstantiater mainUIInstantiater,
        IInstantiater fullScreenUIInstantiater,
        IInstantiater popupsUIInstantiater,
        List<IUIView> windows,
        ILogger logger,
        IAnimatorServiceProvider animatorServiceProvider) : base(mainUIInstantiater,
                                                                fullScreenUIInstantiater,
                                                                popupsUIInstantiater,
                                                                windows,
                                                                animatorServiceProvider)
    {
        _logger = logger;
    }
}
}