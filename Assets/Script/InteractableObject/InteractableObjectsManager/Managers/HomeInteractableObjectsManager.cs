using System.Collections.Generic;
using Script.Initializer;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjectsManager.Managers
{
public class HomeInteractableObjectsManager : MonoBehaviour, IInteractableObjectsManager, IInitializable
{
    [SerializeField]
    private List<InteractableView> _views;
    
    public void Initialize()
    {
        //_views = new List<InteractableView>(viewModels);
    }
}
}