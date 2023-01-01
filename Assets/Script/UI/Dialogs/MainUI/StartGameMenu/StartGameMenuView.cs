using System;
using Script.Libraries.UISystem.UIWindow;
using Script.UI.Dialogs.BaseBehaviour;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.MainUI.StartGameMenu
{
//TODO: raname all start game menu(префабы и тд блять)
public class StartGameMenuView : UiViewBehaviour, IMainUI
{
    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private Button _closeButton;

    [SerializeField] 
    private Button _createCharacterButton;
    
    public event Action QuitPressed;

    public event Action StartPressed;
    public event Action CharacterCreationButtonPressed;

    public override void OnShown()
    {
        _startButton.onClick.AddListener(StartButton);
        _closeButton.onClick.AddListener(CloseButton);
        _createCharacterButton.onClick.AddListener(CreateCharacterButtonPressed);
    }

    public override void OnHidden()
    {
        _startButton.onClick.RemoveListener(StartButton);
        _closeButton.onClick.RemoveListener(CloseButton);
        _createCharacterButton.onClick.RemoveListener(CreateCharacterButtonPressed);
    }
    
    private void CreateCharacterButtonPressed()
    {
        CharacterCreationButtonPressed?.Invoke();
    }

    private void CloseButton()
    {
        QuitPressed?.Invoke();
    }

    private void StartButton()
    {
        StartPressed?.Invoke();
    }
}
}