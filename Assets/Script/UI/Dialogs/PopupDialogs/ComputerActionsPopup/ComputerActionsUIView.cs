using System;
using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.PopupDialogs. ComputerActionsPopup
{
public class ComputerActionsUIView: MonoUiViewPopup
{
    [SerializeField] private Button _closeButton;

    public event Action OnClosePressed;
    
    public override void OnShown()
    {
        base.OnShown();
        
        _closeButton.onClick.AddListener(CloseButton);
    }

    public override void OnHidden()
    {
        base.OnHidden();
        
        _closeButton.onClick.RemoveListener(CloseButton);
    }

    private void CloseButton()
    {
        OnClosePressed?.Invoke();
    }

  
}
}