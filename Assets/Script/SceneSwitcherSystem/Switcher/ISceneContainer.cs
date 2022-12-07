namespace Script.SceneSwitcherSystem.Switcher
{
public interface ISceneContainer
{
    T GetScene<T>() where T : IGameScene;
}
}