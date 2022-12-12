using System;
using Script.InputChecker.Base;
using Script.Libraries.Observer.Base;
using UnityEngine;

namespace Script.InteractableObject.Base
{
public abstract class InteractableBase : MonoBehaviour
{
    protected IObserver Observer { get; private set; }
    protected Canvas Canvas { get; private set; }
    protected IObjectClickChecker mouseClickChecker;

    public void InitializeDependency(IObserver observer, Canvas canvas)
    {
        Observer = observer;
        Canvas = canvas;
    }

    public void InitializeClickInput(IObjectClickChecker objectClickChecker)
    {
        mouseClickChecker = objectClickChecker;
    }

    #region Abstract

    protected abstract void ActivateInput();

    protected abstract void DeactivateInput();

    public abstract event Action ObjectClicked;

    protected abstract void OnClick();

    public abstract Collider ClickTrackCollider { get; }

    #endregion
}
}