using System;
using Script.Libraries.MVVM;
using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterInfo
{
public class CharacterInfoView: MonoUiViewFullscreen
{
    [SerializeField] private Button _closeButton;
    
    public event Action CloseButtonPressed;

    private void Start()
    {
        _closeButton.onClick.AddListener(CloseFullScreen);
    }

    private void CloseFullScreen()
    {
        CloseButtonPressed?.Invoke();
    }
}
}