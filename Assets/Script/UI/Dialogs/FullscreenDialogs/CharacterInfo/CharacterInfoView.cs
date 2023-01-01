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

    public override void OnShown()
    {
        _closeButton.onClick.AddListener(OnCloseButtonPressed);
    }

    public override void OnHidden()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonPressed);
    }

    private void OnCloseButtonPressed()
    {
        CloseButtonPressed?.Invoke();
    }
}
}