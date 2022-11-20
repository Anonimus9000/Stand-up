using Script.InteractableObject.InteractableObjectsManager.Managers;

namespace Script.SceneSwitcher.Container.Scenes.Home
{
public class HomeSceneViewModel
{
    private HomeInteractableObjectsManager _homeInteractableObjectsManager;
    
    public void Initialize(HomeInteractableObjectsManager homeInteractableObjectsManager)
    {
        _homeInteractableObjectsManager = homeInteractableObjectsManager;
        
        //_homeInteractableObjectsManager.Initialize();
    }
}
}