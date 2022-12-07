﻿using System;
using System.Collections.Generic;
using Script.Initializer.Base;
using Script.Initializer.MainInitializers;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Activators;
using Script.SceneSwitcherSystem.Activators.Base;
using Script.SceneSwitcherSystem.Container.Scenes;
using Script.SceneSwitcherSystem.Container.Scenes.Home;
using Script.SceneSwitcherSystem.Switcher;

namespace Script.SceneSwitcherSystem.Container
{
public class SceneContainer : ISceneContainer
{
    private List<IGameScene> _scenes;
    private IUIManager _uiManager;

    public SceneContainer(IUIManager uiManager, IInitializer homeInitializer, IActivator homeLocationActivator)
    {
        InitializeScenes(homeLocationActivator, homeInitializer);
    }

    private void InitializeScenes(IActivator homeLocationActivator, IInitializer homeInitializer)
    {
        var scenes = new IGameScene[]
        {
            new ApplicationLoadingScene(),
            new HomeScene(homeInitializer, homeLocationActivator),
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