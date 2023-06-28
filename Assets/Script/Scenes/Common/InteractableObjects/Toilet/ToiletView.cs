﻿using System;
using Script.ProjectLibraries.MVVM;
using Script.Scenes.Common.InteractableObjects.Base;
using Script.UI.UIFollower.Base;
using UnityEngine;

namespace Script.Scenes.Common.InteractableObjects.Toilet
{
public sealed class ToiletView : InteractableBase, IView
{
    [SerializeField]
    private Collider _clickTrackCollider;

    [SerializeField]
    private Transform _progressBarPosition;

    public override Collider ClickTrackCollider => _clickTrackCollider;
    public Transform ProgressBarPosition => _progressBarPosition;
    public override event Action ObjectClicked;

    private RectTransform _progressBar;
    private IUiFollower _uiFollower;

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