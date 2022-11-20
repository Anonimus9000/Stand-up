using Script.SceneSwitcher.Container;
using Script.SceneSwitcher.Switcher;
using Script.SceneSwitcherComponents;
using UnityEngine;

namespace Script.Initializer.Initializables.StartApplicationInitializables
{
public class LocationSwitcherInitializer : MonoBehaviour, IInitializer
{
    private IInitializable _sceneSwitcher;
    private ISceneContainer _sceneContainer;
    
    public IInitializable InitializeElements()
    {
        _sceneContainer = GetSceneContainer();
        
        _sceneSwitcher = new SceneSwitcher.Switcher.SceneSwitcher(_sceneContainer);

        return _sceneSwitcher;
    }

    private ISceneContainer GetSceneContainer()
    {
        var sceneContainer = new LocationContainer();
        sceneContainer.InitializeScenes();

        return sceneContainer;
    }
}
}