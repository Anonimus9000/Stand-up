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

    private void Start()
    {
        _closeButton.onClick.AddListener(OnCloseButtonPressed);
    }

    public override void Show()
    {
    }

    public override void Hide()
    {
    }

    private void OnCloseButtonPressed()
    {
        CloseButtonPressed?.Invoke();
    }
}
}