using System;
using System.Collections.Generic;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.LocationActionsData;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;
using Script.Scenes.Common.ActionProgressSystem.Handler;
using Script.Scenes.Home.ActionProgressSystem.Handler;
using Script.Scenes.Home.UIs.Popups.ActionsPopup.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Scenes.Home.UIs.Popups.ActionsPopup
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