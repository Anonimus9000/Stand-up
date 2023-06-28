using Script.ProjectLibraries.MVVM;
using Script.Scenes.Common.InteractableObjects.Base;
using UnityEngine;

namespace Script.Scenes.Home.InteractableObjects
{
public class HomeInteractableObjectsView : BehaviourDisposableBase, IView
{
    [field: SerializeReference]
    public Transform ComputerParent { get; private set; }
    
    [field: SerializeField]
    public Transform ToiletParent { get; private set; }
}
}