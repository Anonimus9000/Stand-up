using System;
using Script.ProjectLibraries.InputChecker.Base;
using Script.ProjectLibraries.MVVM;
using Script.Scenes.Common.InteractableObjects.Base;
using UnityEngine;

namespace Script.Scenes.Common.InteractableObjects.Player
{
public sealed class PlayerView : InteractableViewBase
{
    [SerializeField]
    private Collider _clickTrackCollider;

    public override event Action ObjectClicked;

    public override Collider ClickTrackCollider => _clickTrackCollider;

    private IObjectClickChecker _mouseClickChecker;

    protected override void ActivateInput()
    {
    }

    protected override void DeactivateInput()
    {
    }

    protected override void OnClick()
    {
        ObjectClicked?.Invoke();
    }
}
}