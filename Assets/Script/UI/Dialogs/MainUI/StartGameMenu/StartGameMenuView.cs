using System;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.BaseBehaviour;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace Script.UI.Dialogs.MainUI.StartGameMenu
{
public class StartGameMenuView : UiViewBehaviour, IMainUI
{
    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private Button _closeButton;

    [SerializeField] private Button _createCharacterButton;
    
    public event Action OnQuitPressed;

    public event Action OnStartPressed;
    public event Action OnCreatePressed;

    public override event Action ViewShown;
    public override event Action ViewHidden;

    public override void Show()
    {
        _startButton.onClick.AddListener(StartButton);
        _closeButton.onClick.AddListener(CloseButton);
    }

    public override void Hide()
    {
        _startButton.onClick.RemoveListener(StartButton);
        _closeButton.onClick.RemoveListener(CloseButton);
        _createCharacterButton.onClick.AddListener(CreateCharacterButtonPressed);
    }
    
    private void CreateCharacterButtonPressed()
    {
        OnCreatePressed?.Invoke();
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