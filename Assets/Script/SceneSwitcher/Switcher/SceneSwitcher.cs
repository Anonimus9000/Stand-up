using Script.Initializer;

namespace Script.SceneSwitcher.Switcher
{
public class SceneSwitcher : ISceneSwitcher, IInitializable
{
    private readonly ISceneContainer _sceneContainer;
    private IGameLocation _currentLocation;

    public SceneSwitcher(ISceneContainer sceneContainer)
    {
        _sceneContainer = sceneContainer;
    }

    public void Initialize()
    {
    }

    public T SwitchTo<T>() where T : IGameLocation
    {
        _currentLocation.Close();
        _currentLocation.OnClose();
        
        var gameScene = _sceneContainer.GetScene<T>();
        
        gameScene.Initialize();
        gameScene.Open();
        gameScene.OnOpen();

        return gameScene;
    }
}
}