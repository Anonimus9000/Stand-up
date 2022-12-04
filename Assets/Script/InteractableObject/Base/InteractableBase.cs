using System;
using Script.InputChecker.Base;
using Script.Libraries.Observer.Base;
using UnityEngine;

namespace Script.InteractableObject.Base
{
public abstract class InteractableBase : MonoBehaviour
{
    protected IObserver Observer { get; private set; }

    public void InitializeObserver(IObserver observer)
    {
        Observer = observer;
    }

    #region Abstract

    public abstract event Action ObjectClicked;

    protected abstract void OnClick();

    public abstract Collider ClickTrackCollider { get; }

    public abstract void InitializeClickInput(IObjectClickChecker objectClickChecker);

    #endregion
}
}