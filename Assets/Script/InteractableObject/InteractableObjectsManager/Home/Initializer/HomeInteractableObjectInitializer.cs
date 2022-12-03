using System;
using System.Collections.Generic;
using Script.Initializer;
using Script.Initializer.Base;
using Script.InputChecker.Base;
using Script.InputChecker.MouseKeyboard;
using Script.InteractableObject.Base;
using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjectsManager.Home.Initializer
{
public class HomeInteractableObjectInitializer : MonoBehaviour, IDependenciesInitializer
{
    [SerializeReference]
    private List<InteractableBase> _interactableObjects;

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
            Collider trackCollider = null;
            
            if (interactableObject is IView interactableView)
            {
                interactableView.Initialize();
            }
            else
            {
                throw new Exception("Need serialize InteractableView");
            }

            var inputControls = new InputControls();
            inputControls.Enable();
            var mainCamera = Camera.main;
            
            IObjectClickChecker objectClickChecker = new MouseClickChecker(mainCamera,
                inputControls.MouseKeyboard.Mouse, interactableObject.ClickTrackCollider);
            
            interactableObject.InitializeClickInput(objectClickChecker);
        }
    }
}
}