using System;
using Script.ProjectLibraries.InputChecker.Base;
using Script.ProjectLibraries.MVVM;
using Script.Scenes.Common.InteractableObjects.Base;
using UnityEngine;

namespace Script.Scenes.Common.InteractableObjects.Player
{
public sealed class PlayerView : InteractableBase, IView
{
    [SerializeField]
    private Collider _clickTrackCollider;

    public override event Action ObjectClicked;

    public override Collider ClickTrackCollider => _clickTrackCollider;

    private PlayerModel _model;

    private IObjectClickChecker _mouseClickChecker;

    public void InitializeModel(IModel model)
    {
        _model = model as PlayerModel;
    }

    protected override void ActivateInput()
    {
        throw new NotImplementedException();
    }

    protected override void DeactivateInput()
    {
        throw new NotImplementedException();
    }

    protected override void OnClick()
    {
        ObjectClicked?.Invoke();
    }
}
}