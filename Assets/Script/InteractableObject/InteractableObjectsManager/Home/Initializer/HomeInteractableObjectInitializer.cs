using System;
using System.Collections.Generic;
using Script.Initializer;
using Script.Initializer.Base;
using Script.InteractableObject.Base;
using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjectsManager.Home.Initializer
{
public class HomeInteractableObjectInitializer : MonoBehaviour, IDependenciesInitializer
{
    [SerializeReference]
    private List<IInteractable> _interactableObjects;

    public IInitializable Initialize()
    { 
        InitializeInteractableObjects();
        
        //TODO: Replace initialize MVVM to IInitializable object
        return null;
    }

    private void InitializeInteractableObjects()
    {
        foreach (var interactableObject in _interactableObjects)
        {
            if (interactableObject is IView interactableView)
            {
                interactableView.Initialize();
            }
            else
            {
                throw new Exception("Need serialize InteractableView");
            }
        }
    }
}
}