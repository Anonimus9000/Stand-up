using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Container.Scenes;
using Script.SceneSwitcherSystem.Container.Scenes.Home;
using Script.SceneSwitcherSystem.Switcher;
using UnityEngine;

namespace Script.SceneSwitcherSystem.Container
{
public class SceneContainer : MonoBehaviour, ISceneContainer
{
    private List<IGameScene> _scenes;
    private IUIManager _uiManager;

    public void Initialize(IUIManager uiManager)
    {
        InitializeScenes();
    }

    public void InitializeScenes()
    {
        var scenes = new IGameScene[]
        {
            new LoadingScene(),
            new HomeScene(),
            new ConcertScene(),
            new MainMenuScene()
        };
        
        _scenes = new List<IGameScene>(scenes);
    }

    public T GetScene<T>() where T : IGameScene
    {
        foreach (var gameLocation in _scenes)
        {
            if (gameLocation is T location)
            {
                return location;
            }
        }

        throw new Exception("Can't find scene");
    }
}
}