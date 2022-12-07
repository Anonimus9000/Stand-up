using System;
using Script.Libraries.MVVM;
using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.FullscreenDialogs
{
public class CharacterInfoFullScreen: UIViewFullscreen
{
    [SerializeField] private Button _closeButton;
    
    public override IModel Model { get; protected set; }

    private void Start()
    {
        _closeButton.onClick.AddListener(CloseFullScreen);
    }

    private void CloseFullScreen()
    {
        uiManager.Close<CharacterInfoFullScreen>();
    }
}
}