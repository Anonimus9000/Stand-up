namespace Script.SceneSwitcherSystem.Switcher
{
public interface ISceneContainer
{
    void InitializeScenes();
    T GetScene<T>() where T : IGameScene;
}
}