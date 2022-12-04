using System;
using Script.InputChecker.Base;
using Script.InteractableObject.Base;
using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Computer
{
public class ComputerView : InteractableBase, IView
{
    [SerializeField]
    private Collider _clickTrackCollider;

    public override Collider ClickTrackCollider => _clickTrackCollider;
    public override event Action ObjectClicked;

    private ComputerViewModel _viewModel;
    private IObjectClickChecker _mouseClickChecker;

    public void Initialize(IViewModel viewModel)
    {
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