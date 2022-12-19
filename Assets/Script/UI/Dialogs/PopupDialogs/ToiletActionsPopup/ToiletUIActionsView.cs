using System;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.BaseBehaviour;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.PopupDialogs.ToiletActionsPopup
{
public class ToiletUIActionsView: UiViewBehaviour, IPopup
{
    [SerializeField] private Button _closeButton;

    public event Action OnClosePressed;
    public override event Action ViewShown;
    public override event Action ViewHidden;

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