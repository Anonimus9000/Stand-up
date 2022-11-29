using Script.Initializer.Base;
using Script.InteractableObject.InteractableObjectsManager.Home.Initializer;
using UnityEngine;

namespace Script.Initializer.MainInitializers
{
public class HomeInitializer : MonoBehaviour, IMainInitializer
{
    [SerializeReference]
    private IDependenciesInitializer _homeInteractableObjectInitializer;
    
    private void OnEnable()
    {
        InitializeElements();
    }

    public void InitializeElements()
    {
        InitializeHomeInteractableObjects();
    }

    private void InitializeHomeInteractableObjects()
    {
        var initializable = _homeInteractableObjectInitializer.Initialize();
    }
}
}