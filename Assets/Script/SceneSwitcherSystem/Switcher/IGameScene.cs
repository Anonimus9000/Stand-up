using System;
using Script.SceneSwitcherSystem.Container;

namespace Script.SceneSwitcherSystem.Switcher
{
public interface IGameScene
{
    event Action<SceneType> SceneOpened;
    event Action<SceneType> SceneClosed;
    void Initialize();
    void Open();
    void Close();
    void OnOpened();
    void OnClosed();
}
}