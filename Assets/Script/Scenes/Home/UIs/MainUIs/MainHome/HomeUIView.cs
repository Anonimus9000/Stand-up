using System;
using Script.ProjectLibraries.SceneSwitcherSystem;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;
using Script.Scenes.Home.UIs.MainUIs.MainHome.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using IUIServiceLocator = Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider.IUIServiceLocator;

namespace Script.Scenes.Home.UIs.MainUIs.MainHome
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

    [SerializeField]
    private Slider _stressSlider;

    public ProgressBar ProgressBarPrefab => _progressBarPrefab;
    public Transform ProgressBarParen => _progressBarParent;
    public FlyBubble FlyBubblePrefab => _flyBubblePrefab;
    public Transform UpgradePointsIcon => _upgradePointsIcon;
    public Transform BubbleParent => _bubblesParent;
    public TextMeshProUGUI UpgradePoints => _upgradePoints;

    public Slider StressSlider => _stressSlider;

    private IUIServiceLocator _serviceLocator;
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

    public void UpdateStress(float stressPoints)
    {
        var stressValue = _stressSlider.value + stressPoints/100;
        if (stressValue <= 0)
        {
            _stressSlider.value = 0;
        }
        else
        {
            _stressSlider.value = stressValue;
        }
        
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