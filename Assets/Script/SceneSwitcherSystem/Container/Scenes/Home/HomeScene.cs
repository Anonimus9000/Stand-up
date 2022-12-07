using System;
using Script.Initializer.Base;
using Script.Initializer.MainInitializers;
using Script.SceneSwitcherSystem.Activators.Base;
using Script.SceneSwitcherSystem.Switcher;

namespace Script.SceneSwitcherSystem.Container.Scenes.Home
{
public class HomeScene : IGameScene
{
    public event Action<SceneType> SceneOpened;
    public event Action<SceneType> SceneClosed;

    private readonly HomeInitializer _homeInitializer;
    private readonly IActivator _homeActivator;

    public HomeScene(IInitializer homeInitializer, IActivator homeActivator)
    {
        _homeActivator = homeActivator;
        _homeInitializer = homeInitializer as HomeInitializer;
    }

    public void Initialize()
    {
        _homeInitializer.Initialize();
    }

    public void Open()
    {
    }

    public void Close()
    {
        HideHome();
    }

    public void OnOpened()
    {
        ShowHome();

        SceneOpened?.Invoke(SceneType.Home);
    }

    public void OnClosed()
    {
        _homeActivator.Deactivate();

        SceneClosed?.Invoke(SceneType.Home);
    }

    private void ShowHome()
    {
        _homeActivator.Activate();
    }

    private void HideHome()
    {
    }
}
}