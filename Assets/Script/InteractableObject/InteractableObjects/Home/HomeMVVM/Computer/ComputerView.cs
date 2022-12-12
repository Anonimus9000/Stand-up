using System;
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
    private IUiFollower _uiFollower;

    public void InitializeModel(IModel model)
    {
        var camera = Camera.main;

        //Instantiate(_progressBarImage, Canvas.transform);

        //_uiFollower = new UiSpaceObjectFollower(_progressBarPosition, );
    }

    protected override void OnClick()
    {
        ObjectClicked?.Invoke();
    }

    public void ChangeClickInputActive(bool isActive)
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

    protected override void ActivateInput()
    {
        mouseClickChecker.Activate();
        mouseClickChecker.ObjectClicked += OnClick;
    }

    protected override void DeactivateInput()
    {
        mouseClickChecker.Deactivate();
        mouseClickChecker.ObjectClicked -= OnClick;
    }
}
}