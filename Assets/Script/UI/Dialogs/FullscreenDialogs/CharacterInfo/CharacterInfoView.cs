using System;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.BaseBehaviour;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterInfo
{
public class CharacterInfoView : UiViewBehaviour, IFullScreen
{
    [SerializeField]
    private Button _closeButton;

    public event Action CloseButtonPressed;
    public override event Action ViewShown;
    public override event Action ViewHidden;

    public override void Show()
    {
        _closeButton.onClick.AddListener(OnCloseButtonPressed);
        ViewShown?.Invoke();
    }

    public override void Hide()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonPressed);
        ViewHidden?.Invoke();
    }

    private void OnCloseButtonPressed()
    {
        CloseButtonPressed?.Invoke();
    }
}
}