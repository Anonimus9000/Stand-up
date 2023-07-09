using System;
using Script.ProjectLibraries.InputChecker.Base;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.Observer.Base;
using UnityEngine;

namespace Script.Scenes.Common.InteractableObjects.Base
{
public abstract class InteractableViewBase : ViewBehaviour
{
    [SerializeField]
    private Transform _progressBarTransform;

    public Transform ProgressBarTransform => _progressBarTransform;
    protected IObserver Observer { get; private set; }
    protected Canvas Canvas { get; private set; }

    protected IObjectClickChecker mouseClickChecker;

    public void Initialize(IObserver observer, Canvas canvas, IObjectClickChecker objectClickChecker)
    {
        Observer = observer;
        Canvas = canvas;
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