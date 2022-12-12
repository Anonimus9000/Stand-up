using System;
using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.FullscreenDialogs.StartGameMenu
{
public class StartGameView : MonoUIMain
{
    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private Button _closeButton;
    
    public event Action OnQuitPressed;

    public event Action OnStartPressed;

    public override void OnShown()
    {
        base.OnShown();

        _startButton.onClick.AddListener(StartButton);
        _closeButton.onClick.AddListener(CloseButton);
    }

    public override void OnHidden()
    {
        base.OnHidden();

        _startButton.onClick.RemoveListener(StartButton);
        _closeButton.onClick.RemoveListener(CloseButton);
    }

    private void CloseButton()
    {
        OnQuitPressed?.Invoke();
    }

    private void StartButton()
    {
        OnStartPressed?.Invoke();
    }
}
}