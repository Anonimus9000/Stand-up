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
    public void InitializeDependency(IObserver observer, Canvas canvas)
    {
        Observer = observer;
        Canvas = canvas;
    }

    #region Abstract

    public abstract event Action ObjectClicked;

    protected abstract void OnClick();

    public abstract Collider ClickTrackCollider { get; }

    public abstract void InitializeClickInput(IObjectClickChecker objectClickChecker);

    #endregion
}
}