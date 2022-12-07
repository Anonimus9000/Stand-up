using Script.Initializer.Base;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Activators;
using Script.SceneSwitcherSystem.Activators.Base;
using Script.SceneSwitcherSystem.Container;
using Script.SceneSwitcherSystem.Container.Scenes;
using Script.SceneSwitcherSystem.Switcher;
using UnityEngine;
using ILogger = Script.Libraries.Logger.LoggerBase.ILogger;

namespace Script.Initializer.StartApplicationDependenciesInitializers
{
public class SceneSwitcherDependenciesInitializer : IDependenciesInitializer
{
    private ISceneSwitcher _sceneSwitcher;
    private ISceneContainer _sceneContainer;
    private readonly IUIManager _uiManager;
    private readonly GameObject _homeGameObject;
    private readonly IInitializer _homeInitializer;
    private readonly ILogger _logger;

    public SceneSwitcherDependenciesInitializer(IUIManager uiManager, ISceneContainer sceneContainer,
        IInitializer homeInitializer, GameObject homeGameObject, ILogger logger)
    {
        _logger = logger;
        _homeGameObject = homeGameObject;
        _homeInitializer = homeInitializer;
        _uiManager = uiManager;
        _sceneContainer = sceneContainer;
    }

    public IInitializable Initialize()
    {
        _sceneContainer = InitializeSceneContainer(_uiManager, _homeInitializer, _homeGameObject);

        _sceneSwitcher = new SceneSwitcher(_sceneContainer, _logger);

        OpenApplicationDotScene(_sceneSwitcher);

        return _sceneSwitcher as IInitializable;
    }

    private static ISceneContainer InitializeSceneContainer(IUIManager uiManager, IInitializer homeInitializer,
        GameObject homeGameObject)
    {
        IActivator activator = new HomeLocationActivator(homeGameObject);

        var sceneContainer = new SceneContainer(uiManager, homeInitializer, activator);

        return sceneContainer;
    }

    private void OpenApplicationDotScene(ISceneSwitcher sceneSwitcher)
    {
        sceneSwitcher.SwitchTo<MainMenuScene>();
    }
}
}