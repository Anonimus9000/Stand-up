using System;
using Script.InputChecker.Base;
using Script.InteractableObject.Base;
using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Common.Player.PlayerMVVM
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