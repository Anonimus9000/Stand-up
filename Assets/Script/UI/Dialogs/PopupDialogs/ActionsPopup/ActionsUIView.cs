using System;
using System.Collections.Generic;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.BaseBehaviour;
using Script.UI.Dialogs.MainUI.MainHome;
using Script.UI.Dialogs.PopupDialogs.Components;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.UI.Dialogs.PopupDialogs.ActionsPopup
{
public class ActionsUIView : UiViewBehaviour, IPopup
{
    [SerializeField]
    private Button _closeButton;

    [FormerlySerializedAs("_actionFields")] [FormerlySerializedAs("_actionFieldsSetter")] [SerializeField] private ActionFields _actionFieldPrefab;
    [SerializeField] private Transform _actionTransform;
    
    public event Action OnClosePressed;
    public override event Action ViewShown;
    public override event Action ViewHidden;



    public void Init(
        List<ActionFieldContent> fields,
        MainUIService mainUIService,
        ActionProgressHandler actionProgressHandler,
        Vector3 position) 
    {
        foreach (var actionField in fields)
        {
            var actionFields = Instantiate(_actionFieldPrefab, _actionTransform);
            actionFields.Init(actionField.ActionIcon,
                actionField.ActionTitle,
                actionField.ActionRewards,
                actionField.ActionTime,
                mainUIService,
                actionProgressHandler,
                position);
        }
    }
    
    public override void Show()
    { 
        _closeButton.onClick.AddListener(CloseButton);

    }
    


    public override void Hide()
    {
        _closeButton.onClick.RemoveListener(CloseButton);
    }

    private void CloseButton()
    {
        OnClosePressed?.Invoke();
    }
}
}