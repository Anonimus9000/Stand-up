using Script.Initializer.Base;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.SceneSwitcherSystem.Container;
using Script.SceneSwitcherSystem.Switcher;

namespace Script.Initializer.StartApplicationDependenciesInitializers
{
public class SceneSwitcherDependenciesInitializer : IDependenciesInitializer
{
    private IInitializable _sceneSwitcher;
    private ISceneContainer _sceneContainer;
    private readonly IUIManager _uiManager;

    public SceneSwitcherDependenciesInitializer(IUIManager uiManager, ISceneContainer sceneContainer)
    {
        _uiManager = uiManager;
        _sceneContainer = sceneContainer;
    }
    
    public IInitializable Initialize()
    {
        _sceneContainer = InitializeSceneContainer();
        
        _sceneSwitcher = new SceneSwitcher(_sceneContainer);

        return _sceneSwitcher;
    }

    private ISceneContainer InitializeSceneContainer()
    {
        (_sceneContainer as SceneContainer)!.Initialize(_uiManager);

        return _sceneContainer;
    }
}
}