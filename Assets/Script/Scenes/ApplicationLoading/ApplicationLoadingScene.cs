using System;
using System.Threading.Tasks;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.SceneSwitcherSystem;

namespace Script.Scenes.ApplicationLoading
{
public class ApplicationLoadingScene : ViewModel, IScene
{
    public event Action<SceneType> SceneOpened;
    public event Action<SceneType> SceneClosed;

    public void Open()
    {
    }

    public void Close()
    {
    }

    public Task OpenAsync()
    {
        return Task.CompletedTask;
    }

    public Task CloseAsync()
    {
        return Task.CompletedTask;
    }
}
}