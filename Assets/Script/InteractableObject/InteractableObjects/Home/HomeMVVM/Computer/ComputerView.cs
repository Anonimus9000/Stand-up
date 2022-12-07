using System;
using Script.InputChecker.Base;
using Script.InteractableObject.Base;
using Script.Libraries.MVVM;
using Script.UI.UIFollower.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Script.InteractableObject.InteractableObjects.Home.HomeMVVM.Computer
{
public class ComputerView : InteractableBase, IView
{
    [SerializeField]
    private Collider _clickTrackCollider;

    [SerializeField]
    private Transform _progressBarPosition;

    [SerializeField]
    private Image _progressBarImage;

    public override Collider ClickTrackCollider => _clickTrackCollider;
    public override event Action ObjectClicked;

    private RectTransform _progressBar;
    private ComputerModel _model;
    private IObjectClickChecker _mouseClickChecker;
    private IUiFollower _uiFollower;

    public void Initialize(IModel model)
    {
        _model = model as ComputerModel;

        var camera = Camera.main;

        SubscribeOnModelEvents();
        
        //Instantiate(_progressBarImage, Canvas.transform);

        //_uiFollower = new UiSpaceObjectFollower(_progressBarPosition, );
    }

    public override void InitializeClickInput(IObjectClickChecker objectClickChecker)
    {
        _mouseClickChecker = objectClickChecker;
    }

    protected override void OnClick()
    {
        ObjectClicked?.Invoke();
    }

    #region ModelEvents

    private void SubscribeOnModelEvents()
    {
        _model.InputActiveChanged += OnInputActiveChanged;
    }

    private void OnInputActiveChanged(bool isActive)
    {
        if (isActive)
        {
            ActivateInput();
        }
        else
        {
            DeactivateInput();
        }
    }

    #endregion

    private void ActivateInput()
    {
        _mouseClickChecker.Activate();
        _mouseClickChecker.ObjectClicked += OnClick;
    }

    private void DeactivateInput()
    {
        _mouseClickChecker.Deactivate();
        _mouseClickChecker.ObjectClicked -= OnClick;
    }
}
}