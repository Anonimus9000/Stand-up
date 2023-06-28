using System.Threading.Tasks;
using Script.ProjectLibraries.Logger.LoggerBase;
using Script.ProjectLibraries.MVVM;

namespace Script.ProjectLibraries.SceneSwitcherSystem
{
public class SceneSwitcherViewModel : ViewModel, ISceneSwitcher
{
    private readonly ISceneModel _sceneModel;
    private IScene _currentScene;
    private readonly ILogger _logger;

    public SceneSwitcherViewModel(
        ILogger logger, 
        ISceneModel sceneModel)
    {
        _sceneModel = sceneModel;
        _logger = logger;
    }

    public T SwitchTo<T>() where T : IScene
    {
        if (_currentScene is T scene)
        {
            return scene;
        }
        
        TryCloseCurrentScene();

        var gameScene = _sceneModel.GetScene<T>();
        
        _currentScene = gameScene;

        gameScene.Open();
        
        _logger.Log($"Scene {typeof(T)} opened");

        return gameScene;
    }

    public async Task<T> SwitchToAsync<T>() where T : IScene
    {
        if (_currentScene is T scene)
        {
            return scene;
        }
        
        await TryCloseCurrentSceneAsync();

        var gameScene = _sceneModel.GetScene<T>();
        
        await gameScene.OpenAsync();

        _currentScene = gameScene;
        
        _logger.Log($"Scene {typeof(T)} opened");

        return gameScene;
    }

    private bool TryCloseCurrentScene()
    {
        if (_currentScene != null)
        {
            _currentScene.Close();

            _logger.Log($"Scene {_currentScene.GetType()} closed");

            return true;
        }

        return false;
    }

    private async Task<bool> TryCloseCurrentSceneAsync()
    {
        if (_currentScene != null)
        { 
            await _currentScene.CloseAsync();

            _logger.Log($"Scene {_currentScene.GetType()} closed");

            return true;
        }

        return false;
    }
}
}