namespace Script.SceneSwitcher.Switcher
{
public interface ISceneContainer
{
    void InitializeScenes();
    T GetScene<T>() where T : IGameLocation;
}
}