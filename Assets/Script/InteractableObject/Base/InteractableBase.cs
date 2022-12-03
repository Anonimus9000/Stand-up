using System;
using Script.InputChecker.Base;
using UnityEngine;

namespace Script.InteractableObject.Base
{
public abstract class InteractableBase : MonoBehaviour
{
    public abstract event Action ObjectClicked; 
    protected abstract void OnClick();
    public abstract Collider ClickTrackCollider { get; }
    public abstract void InitializeClickInput(IObjectClickChecker objectClickChecker);
}
}