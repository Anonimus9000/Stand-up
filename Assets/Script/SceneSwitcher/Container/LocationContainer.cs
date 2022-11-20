using System;
using System.Collections.Generic;
using Script.SceneSwitcher.Container.Scenes.Home;
using Script.SceneSwitcher.Switcher;
using Script.SceneSwitcherComponents.Scenes;

namespace Script.SceneSwitcher.Container
{
public class LocationContainer : ISceneContainer
{
    private List<IGameLocation> _locations;

    public void InitializeScenes()
    {
        var allScenes = GetAllLocations();
        _locations = new List<IGameLocation>(allScenes);
    }

    public T GetScene<T>() where T : IGameLocation
    {
        foreach (var gameLocation in _locations)
        {
            if (gameLocation is T location)
            {
                return location;
            }
        }

        throw new Exception("Can't find scene");
    }

    private void LoadLocations()
    {
        
    }

    private IEnumerable<IGameLocation> GetAllLocations()
    {
        return new IGameLocation[]
        {
            new LoadingLocation(),
            new HomeLocationView(),
            new ConcertLocation(),
            new MainMenuLocation()
        };
    }
}
}