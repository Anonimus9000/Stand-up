using System;
using Script.InputChecker.Base;
using Script.Libraries.Observer.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.InteractableObject.Base
{
public abstract class InteractableBase : MonoBehaviour
{
    [SerializeField]
    private Transform _progressBarTransform;

    public Transform ProgressBarTransform => _progressBarTransform;
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