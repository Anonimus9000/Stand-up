using System;
using System.Collections.Generic;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.BaseBehaviour;
using Script.UI.Dialogs.PopupDialogs.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.PopupDialogs.ActionsPopup
{
public class ActionsUIView : UiViewBehaviour, IPopup
{
    [SerializeField]
    private Button _closeButton;

    [SerializeField] private ActionFieldsSetter _actionFieldsSetter;
    [SerializeField] private Transform _actionTransform;
    

    public event Action OnClosePressed;

    public override event Action ViewShown;
    public override event Action ViewHidden;



    public void Init(List<ActionField> fields)
    {
        foreach (var actionField in fields)
        {
            var actionFieldsSetter = Instantiate(_actionFieldsSetter, _actionTransform);
            actionFieldsSetter.Init(actionField.ActionIcon,actionField.ActionTitle, actionField.ActionRewards);
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