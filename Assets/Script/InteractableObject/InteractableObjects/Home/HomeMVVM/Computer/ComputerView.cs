using System;
using Script.InputChecker.Base;
using Script.InteractableObject.Base;
using Script.Libraries.MVVM;
using Script.UI.UIFollower.Base;
using Script.UI.UIFollower.SpaceObjectFollower;
using UnityEngine;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Computer
{
public class ComputerView : InteractableBase, IView
{
    [SerializeField]
    private Collider _clickTrackCollider;

    [SerializeField]
    private Transform _progressBarPosition;

    [SerializeField]
    private RectTransform _progressBar;

    public override Collider ClickTrackCollider => _clickTrackCollider;
    public override event Action ObjectClicked;

    private ComputerViewModel _viewModel;
    private IObjectClickChecker _mouseClickChecker;
    private IUiFollower _uiFollower;

    private void OnEnable()
    {
        ActivateInput();
    }

    private void OnDisable()
    {
        DeactivateInput();
    }

    public void Initialize(IViewModel viewModel)
    {
        _viewModel = viewModel as ComputerViewModel;

        var camera = Camera.main;
        
        //_uiFollower = new UiSpaceObjectFollower();
    }

    public override void InitializeClickInput(IObjectClickChecker objectClickChecker)
    {
        _mouseClickChecker = objectClickChecker;
        //TODO: replace in activate outside
        ActivateInput();
    }

    private void ActivateInput()
    {
        //TODO: remove if
        if (_mouseClickChecker != null)
        {
            _mouseClickChecker.Activate();
            _mouseClickChecker.ObjectClicked += OnClick;
        }
    }

    private void DeactivateInput()
    {
        //TODO: remove if
        if (_mouseClickChecker != null)
        {
            _mouseClickChecker.Deactivate();
            _mouseClickChecker.ObjectClicked -= OnClick;
        }
    }

    protected override void OnClick()
    {
        ObjectClicked?.Invoke();
    }
}
}