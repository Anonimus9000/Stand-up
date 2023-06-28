using System.Threading.Tasks;

namespace Script.ProjectLibraries.SceneSwitcherSystem
{
public interface ISceneSwitcher
{
    T SwitchTo<T>() where T : IScene;
    Task<T> SwitchToAsync<T>() where T : IScene;
}
}