namespace Script.SceneSwitcher.Switcher
{
public interface ISceneSwitcher
{
    T SwitchTo<T>() where T : IGameLocation;
}
}