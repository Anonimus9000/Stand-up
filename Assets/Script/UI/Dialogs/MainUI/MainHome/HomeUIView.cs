using System;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Dialogs.BaseBehaviour;
using Script.UI.Dialogs.FullscreenDialogs.CharacterInfo;
using Script.UI.Dialogs.MainUI.MainHome.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.MainUI.MainHome
{
public class HomeUIView : UiViewBehaviour, IMainUI
{
    [SerializeField]
    private Button _openFullscreenButton;

    [SerializeField]
    private Button _openCharacterInfoButton;

    [SerializeField]
    private Transform _upgradePointsIcon;

    [SerializeField]
    private FlyBubble _flyBubblePrefab;

    [SerializeField]
    private Transform _bubblesParent;

    private IUIServiceProvider _serviceProvider;
    private FullScreensUIService _fullScreensUIService;
    private ISceneSwitcher _sceneSwitcher;

    public override event Action ViewShown;
    public override event Action ViewHidden;
    public event Action<int> MoveBubbleCompleted;
    public event Action OpenStartGameMenuButtonPressed;
    public event Action OpenCharacterInfoButtonPressed;

    private void CharacterInfoButton()
    {
        _openFullscreenButton.onClick.AddListener(OpenStartFullscreen);
        _openCharacterInfoButton.onClick.AddListener(OpenCharacterInfo);
    }

    public override void OnShown()
    {
        SubscribeOnEvents();
        
        ViewShown?.Invoke();
    }

    public override void Hide()
    {
        UnsubscribeOnEvents();
        
        ViewHidden?.Invoke();
    }

    public void ShowMoveBubble(Vector3 startPosition, int bodyInfo)
    {
        var flyBubble = Instantiate(_flyBubblePrefab, _bubblesParent);
        var upgradePointsLocalPosition = _upgradePointsIcon.transform.position;
        
        flyBubble.ShowAndMoveBubble(startPosition, upgradePointsLocalPosition, bodyInfo);
        
        flyBubble.MoveCompleted += OnMoveBubblePrefabCompleted;
    }

    private void SubscribeOnEvents()
    {
        _openMenuButton.onClick.AddListener(MenuButtonPressed);
        _openCharacterInfoButton.onClick.AddListener(CharacterInfoButton);
    }

    private void UnsubscribeOnEvents()
    {
        _openMenuButton.onClick.RemoveListener(MenuButtonPressed);
        _openCharacterInfoButton.onClick.RemoveListener(CharacterInfoButton);
    }

    private void OpenCharacterInfo()
    {
    }

    private void OpenStartFullscreen()
    {
    }
    
    private void OnOpenStartGameMenuButtonPressed()
    {
        OpenStartGameMenuButtonPressed?.Invoke();
    }

    private void OnOpenCharacterButtonPressed()
    {
        OpenCharacterInfoButtonPressed?.Invoke();
    }
    
    private void MenuButtonPressed()
    {
        OpenStartGameMenuButtonPressed?.Invoke();
    }

    private void OnMoveBubblePrefabCompleted(FlyBubble bubble)
    {
        var bubbleBodyInfo = bubble.BodyInfo;
        MoveBubbleCompleted?.Invoke(bubbleBodyInfo);
        
        bubble.Destroy();
        bubble.MoveCompleted -= OnMoveBubblePrefabCompleted;
    }
}
}