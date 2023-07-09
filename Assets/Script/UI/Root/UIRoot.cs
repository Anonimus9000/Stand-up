using System.Collections.Generic;
using Script.Initializer.Base;
using Script.ProjectLibraries.ResourceLoader;
using Script.ProjectLibraries.SceneSwitcherSystem;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider;
using Script.ProjectLibraries.UISystem.UIWindow;
using Script.UI.AnimatorServiceProvider;
using Script.UI.UIInstantiater;
using Script.UI.UIWindowsLoader;
using UnityEngine;
using ILogger = Script.ProjectLibraries.Logger.LoggerBase.ILogger;

namespace Script.UI.Root
{
public class UIRoot : MonoBehaviour, IRoot
{
    [SerializeField]
    private Transform _parentToCreateMainUI;

    [SerializeField]
    private Transform _parentToCreateFullScreenUI;

    [SerializeField]
    private Transform _parentToCreatePopupsUI;

    [SerializeField]
    private string _pathToDialogs;

    [SerializeField]
    private Canvas _mainCanvas;

    [SerializeField]
    private UiAnimatorServiceProvider _animatorInitializer;

    public Canvas MainCanvas => _mainCanvas;

    private ISceneSwitcher _sceneSwitcher;

    public UIServiceLocator Initialize(ILogger logger, IResourceLoader resourceLoader)
    {
        var uiManager = InitializeUIManager(logger, _animatorInitializer, resourceLoader);

        return uiManager;
    }

    private UIServiceLocator InitializeUIManager(
        ILogger logger,
        UiAnimatorServiceProvider animatorInitializer,
        IResourceLoader resourceLoader)
    {
        var uiWindows = InitializeWindowsLoader(resourceLoader);
        var uiAnimatorService = animatorInitializer.InitializeAnimatorServiceProvider();
        IInstantiater instantiaterMainUI = new UnityInstantiater(_parentToCreateMainUI);
        IInstantiater instantiaterFullScreens = new UnityInstantiater(_parentToCreateFullScreenUI);
        IInstantiater instantiaterPopups = new UnityInstantiater(_parentToCreatePopupsUI);

        return new UIServiceLocator(
            instantiaterMainUI,
            instantiaterFullScreens,
            instantiaterPopups,
            uiWindows,
            uiAnimatorService);
    }

    private List<IUIView> InitializeWindowsLoader(IResourceLoader resourceLoader)
    {
        var unityUIWindowsLoader = new UnityUIWindowsLoader(resourceLoader);

        unityUIWindowsLoader.LoadDialogs(_pathToDialogs);

        return unityUIWindowsLoader.UIWindows;
    }
}
}