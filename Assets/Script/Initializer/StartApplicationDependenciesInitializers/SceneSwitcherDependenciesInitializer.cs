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
    private readonly GameObject _homeGameObject;
    private readonly IInitializer _homeInitializer;
    private readonly ILogger _logger;

    public SceneSwitcherDependenciesInitializer(ISceneContainer sceneContainer,
        IInitializer homeInitializer, GameObject homeGameObject, ILogger logger)
    {
        _logger = logger;
        _homeGameObject = homeGameObject;
        _homeInitializer = homeInitializer;
        _sceneContainer = sceneContainer;
    }

    public IInitializable Initialize()
    {
        _sceneContainer = InitializeSceneContainer(_homeInitializer, _homeGameObject);

        _sceneSwitcher = new SceneSwitcher(_sceneContainer, _logger);

        return _sceneSwitcher as IInitializable;
    }

    private static ISceneContainer InitializeSceneContainer(IInitializer homeInitializer,
        GameObject homeGameObject)
    {
        IActivator activator = new HomeLocationActivator(homeGameObject);

        var sceneContainer = new SceneContainer(homeInitializer, activator);

        return sceneContainer;
    }
}
}