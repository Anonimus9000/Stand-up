using Script.Initializer;
using UnityEngine;
using ILogger = Script.Libraries.Logger.LoggerBase.ILogger;

namespace Script.SceneSwitcherSystem.Switcher
{
public class SceneSwitcher : ISceneSwitcher, IInitializable
{
    private readonly ISceneContainer _sceneContainer;
    private IGameScene _currentScene;
    private readonly ILogger _logger;

    public SceneSwitcher(ISceneContainer sceneContainer, ILogger logger)
    {
        _logger = logger;
        _sceneContainer = sceneContainer;
    }

    public T SwitchTo<T>() where T : IGameScene
    {
        TryCloseCurrentScene();

        var gameScene = _sceneContainer.GetScene<T>();
        
        gameScene.Initialize();
        gameScene.Open();
        gameScene.OnOpened();

        _currentScene = gameScene;
        
        _logger.Log($"Scene {typeof(T)} opened");

        return gameScene;
    }

    private bool TryCloseCurrentScene()
    {
        if (_currentScene != null)
        {
            _currentScene.Close();
            _currentScene.OnClosed();
            
            _logger.Log($"Scene {_currentScene.GetType()} closed");

            return true;
        }

        return false;
    }
}
}