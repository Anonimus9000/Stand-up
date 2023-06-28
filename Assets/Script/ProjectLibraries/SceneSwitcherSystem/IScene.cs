using System;
using System.Threading.Tasks;

namespace Script.ProjectLibraries.SceneSwitcherSystem
{
public interface IScene
{
    event Action<SceneType> SceneOpened;
    event Action<SceneType> SceneClosed;
    void Open();
    void Close();
    Task OpenAsync();
    Task CloseAsync();
}
}