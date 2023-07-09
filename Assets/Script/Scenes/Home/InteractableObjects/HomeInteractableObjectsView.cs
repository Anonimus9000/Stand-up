using Script.ProjectLibraries.MVVM;
using Script.Scenes.Common.InteractableObjects.Base;
using UnityEngine;

namespace Script.Scenes.Home.InteractableObjects
{
public class HomeInteractableObjectsView : ViewBehaviour
{
    [field: SerializeReference]
    public Transform ComputerParent { get; private set; }
    
    [field: SerializeField]
    public Transform ToiletParent { get; private set; }
}
}