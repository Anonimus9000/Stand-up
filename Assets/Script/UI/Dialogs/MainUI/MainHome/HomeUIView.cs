using System;
using Script.Libraries.UISystem.Managers.UiServiceProvider;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Dialogs.BaseBehaviour;
using Script.UI.Dialogs.MainUI.MainHome.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.MainUI.MainHome
{
//TODO: refactoring names by Katya
public class HomeUIView : UiViewBehaviour, IMainUI
{
    [SerializeField]
    private Button _openFullscreenButton;

    [SerializeField]
    private Button _openCharacterInfoButton;

    [SerializeField] 
    private Button _openMenuButton;

    [SerializeField]
    private Transform _upgradePointsIcon;

    [SerializeField]
    private FlyBubble _flyBubblePrefab;

    [SerializeField]
    private Transform _bubblesParent;

    [SerializeField]
    private ProgressBar _progressBarPrefab;

    [SerializeField]
    private Transform _progressBarParent;

    private IUIServiceProvider _serviceProvider;
    private FullScreensUIService _fullScreensUIService;
    private ISceneSwitcher _sceneSwitcher;

    public event Action<int> MoveBubbleCompleted;
    public event Action OpenStartGameMenuButtonPressed;
    public event Action OpenCharacterInfoButtonPressed;
    public event Action ProgressCompleted; 


    public override void OnShown()
    {
        SubscribeOnEvents();
    }

    public override void OnHidden()
    {
        UnsubscribeOnEvents();
    }

    public void ShowProgressBar(float duration, Vector2 screenPosition)
    {
        var progressBar = Instantiate(_progressBarPrefab, _progressBarParent);
        progressBar.transform.position = screenPosition;
        
        progressBar.ShowProgress(duration);
    }

    public void CloseProgressBar()
    {
        _progressBarPrefab.HideProgressBar();
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
        _openCharacterInfoButton.onClick.AddListener(OnOpenCharacterInfoButtonPressed);
    }

    private void UnsubscribeOnEvents()
    {
        _openMenuButton.onClick.RemoveListener(MenuButtonPressed);
        _openCharacterInfoButton.onClick.RemoveListener(OnOpenCharacterInfoButtonPressed);
    }

    private void OnOpenCharacterInfoButtonPressed()
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