﻿using System;
using System.Collections.Generic;
using Script.InteractableObject.ActionProgressSystem;
using Script.InteractableObject.ActionProgressSystem.Handler;
using Script.Libraries.UISystem.Managers.UiServiceProvider;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.BaseBehaviour;
using Script.UI.Dialogs.PopupDialogs.ActionsPopup.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.PopupDialogs.ActionsPopup
{
public class ActionsUIView : UiViewBehaviour, IPopup
{
    [SerializeField]
    private Button _closeButton;

    [SerializeField]
    private ActionFieldItemView _actionFieldItemViewPrefab;

    [SerializeField]
    private Transform _actionTransform;

    public event Action ClosePressed;
    
    public void Init(
        List<ActionFieldData> fields,
        IUIService mainUIService,
        IUIService popupService,
        HomeActionProgressHandler homeActionProgressHandler,
        Vector3 progressBarPosition)
    {
        foreach (var actionField in fields)
        {
            var actionFields = Instantiate(_actionFieldItemViewPrefab, _actionTransform);
            actionFields.Init(actionField.ActionIcon,
                actionField.ActionTitle,
                actionField.ActionRewards,
                actionField.ActionTime,
                mainUIService,
                popupService,
                homeActionProgressHandler,
                progressBarPosition,
                actionField.UpgradePoints);
        }
    }

    public override void OnShown()
    {
        _closeButton.onClick.AddListener(CloseButton);
    }

    public override void OnHidden()
    {
        _closeButton.onClick.RemoveListener(CloseButton);
    }

    private void CloseButton()
    {
        ClosePressed?.Invoke();
    }
}
}