namespace Script.SceneSwitcherSystem.Switcher
{
public interface ISceneSwitcher
{
    T SwitchTo<T>() where T : IGameScene;
}
}