using System;
using Script.InputChecker.Base;
using Script.InteractableObject.Base;
using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Common.Player.PlayerMVVM
{
public class PlayerView : InteractableBase, IView
{
    [SerializeField]
    private Collider _clickTrackCollider;
    
    public override event Action ObjectClicked;
    public override Collider ClickTrackCollider => _clickTrackCollider;

    private PlayerModel _model;
    
    private IObjectClickChecker _mouseClickChecker;

    public void Initialize(IModel model)
    {
        _model = model as PlayerModel;
    }

    public override void InitializeClickInput(IObjectClickChecker objectClickChecker)
    {
        _mouseClickChecker = objectClickChecker;
        
        _mouseClickChecker.Activate();
        _mouseClickChecker.ObjectClicked += OnClick;
    }

    protected override void OnClick()
    {
        ObjectClicked?.Invoke();
    }
}
}