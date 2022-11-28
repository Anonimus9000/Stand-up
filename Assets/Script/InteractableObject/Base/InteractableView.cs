using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.InteractableObject.Base
{
public abstract class InteractableView : MonoBehaviour, IView, IInteractable
{
    public IViewModel ViewModel { get; protected set; }

    public void Initialize(InteractableViewModel viewModel)
    {
        ViewModel = viewModel;
    }

    public abstract void OnClick();
}
}