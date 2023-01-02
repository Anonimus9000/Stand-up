using System;
using Script.Libraries.UISystem.Managers.UiServiceProvider;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Switcher;
using Script.UI.Dialogs.BaseBehaviour;
using Script.UI.Dialogs.MainUI.MainHome.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.MainUI.MainHome
{
//TODO: refactoring. Move all login in model
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

    [SerializeField]
    private TextMeshProUGUI _upgradePoints;

    public ProgressBar ProgressBarPrefab => _progressBarPrefab;
    public Transform ProgressBarParen => _progressBarParent;
    public FlyBubble FlyBubblePrefab => _flyBubblePrefab;
    public Transform UpgradePointsIcon => _upgradePointsIcon;
    public Transform BubbleParent => _bubblesParent;
    public TextMeshProUGUI UpgradePoints => _upgradePoints;

    private IUIServiceProvider _serviceProvider;
    private FullScreensUIService _fullScreensUIService;
    private ISceneSwitcher _sceneSwitcher;

    public event Action OpenStartGameMenuButtonPressed;
    public event Action OpenCharacterInfoButtonPressed;

    public override void OnShown()
    {
        SubscribeOnEvents();
    }

    public override void OnHidden()
    {
        UnsubscribeOnEvents();
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
}
}