namespace Script.ProjectLibraries.SceneSwitcherSystem
{
public interface ISceneModel
{
    T GetScene<T>() where T : IScene;
}
}