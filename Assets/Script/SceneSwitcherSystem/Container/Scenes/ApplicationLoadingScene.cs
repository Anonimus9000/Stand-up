using System;
using Script.SceneSwitcherSystem.Switcher;

namespace Script.SceneSwitcherSystem.Container.Scenes
{
public class ApplicationLoadingScene : IGameScene
{
    public event Action<SceneType> SceneOpened;
    public event Action<SceneType> SceneClosed;

    public void Initialize()
    {
    }

    public void Open()
    {
    }

    public void Close()
    {
    }

    public void OnOpened()
    {
        SceneOpened?.Invoke(SceneType.ApplicationLoading);
    }

    public void OnClosed()
    {
        SceneClosed?.Invoke(SceneType.ApplicationLoading);
    }
}
}