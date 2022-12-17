using System;
using Script.UI.Dialogs.BaseDialogs;
using Script.UI.Dialogs.FullscreenDialogs.CharacterInfo;
using Script.UI.Dialogs.FullscreenDialogs.StartGameMenu;
using Script.UI.Dialogs.MainUI.MainHome.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.MainUI.MainHome
{
public class HomeUIView : MonoUIMain
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

    public event Action<int> MoveBubbleCompleted;

    private void Start()
    {
        _openFullscreenButton.onClick.AddListener(OpenStartFullscreen);
        _openCharacterInfoButton.onClick.AddListener(OpenCharacterInfo);
    }

    public override void OnShown()
    {
        base.OnShown();
        
        SubscribeOnEvents();
    }

    public override void OnClose()
    {
        base.OnClose();
        
        UnsubscribeOnEvents();
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
    }

    private void UnsubscribeOnEvents()
    {
    }

    private void OpenCharacterInfo()
    {
        uiSystem.Show(new CharacterInfoViewModel());
    }

    private void OpenStartFullscreen()
    {
        uiSystem.Show(new StartGameMenuViewModel(uiSystem));
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