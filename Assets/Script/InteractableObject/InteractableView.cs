using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.InteractableObject
{
public abstract class InteractableView : MonoBehaviour, IView
{
    public InteractableViewModel ViewModel { get; protected set; }
    
    protected abstract void OnClick();
}
}