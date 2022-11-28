using Script.Initializer;

namespace Script.SceneSwitcherSystem.Switcher
{
public class SceneSwitcher : ISceneSwitcher, IInitializable
{
    private readonly ISceneContainer _sceneContainer;
    private IGameScene _currentScene;

    public SceneSwitcher(ISceneContainer sceneContainer)
    {
        _sceneContainer = sceneContainer;
    }

    public T SwitchTo<T>() where T : IGameScene
    {
        _currentScene.Close();
        _currentScene.OnClose();
        
        var gameScene = _sceneContainer.GetScene<T>();
        
        gameScene.Initialize();
        gameScene.Open();
        gameScene.OnOpen();

        return gameScene;
    }

    private void InitializeScenes()
    {
        
    }
}
}