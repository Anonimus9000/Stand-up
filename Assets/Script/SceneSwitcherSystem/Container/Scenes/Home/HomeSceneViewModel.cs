using Script.InteractableObject.InteractableObjectsManager.Managers;

namespace Script.SceneSwitcherSystem.Container.Scenes.Home
{
public class HomeSceneViewModel
{
    private HomeInteractableObjectInitializer _homeInteractableObjectInitializer;
    
    public void Initialize(HomeInteractableObjectInitializer homeInteractableObjectInitializer)
    {
        _homeInteractableObjectInitializer = homeInteractableObjectInitializer;
        
        //_homeInteractableObjectsManager.Initialize();
    }
}
}