using System.Collections.Generic;
using Script.ConfigData.LocationActionsConfig;
using Script.InteractableObject.ActionProgressSystem;
using Script.InteractableObject.ActionProgressSystem.Handler;
using Script.Libraries.UISystem.Managers.UiServiceProvider;
using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.UI.Dialogs.MainUI.MainHome;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.UI.Dialogs.PopupDialogs.ActionsPopup.Components
{
public class ActionFieldItemView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _actionTitle;

    [SerializeField]
    private TextMeshProUGUI _actionDescription;

    [SerializeField]
    private Image _actionIcon;

    [FormerlySerializedAs("StartActionButton")]
    [SerializeField]
    public Button _startActionButton;

    private HomeUIViewModel _homeUIViewModel;
    private IUIService _mainUiService;
    private HomeActionProgressHandler _homeActionProgressHandler;
    private float _actionTime;
    private Vector3 _progressBarPosition;
    private IUIService _popupService;
    private int _upgradePoints;
    private List<ActionRewardData> _actionRewards;

    public void Init(
        Sprite actionIcon,
        string actionTitle,
        List<ActionRewardData> actionDescription,
        float actionTime,
        IUIService mainUIService,
        IUIService popupService,
        HomeActionProgressHandler homeActionProgressHandler,
        Vector3 progressBarPosition,
        int upgradePoints)
    {
        _actionDescription.text = string.Empty;
        _actionTitle.text = actionTitle;
        _actionIcon.sprite = actionIcon;
        _startActionButton.onClick.AddListener(OnStartButtonPressed);
        _mainUiService = mainUIService;
        _popupService = popupService;
        _homeActionProgressHandler = homeActionProgressHandler;
        _actionTime = actionTime;
        _progressBarPosition = progressBarPosition;
        _upgradePoints = upgradePoints;

        foreach (var actionRewards in actionDescription)
        {
            _actionDescription.text += actionRewards.RewardValue + " " + actionRewards.RewardTitle + "\n";
        }

        _actionRewards = new List<ActionRewardData>(actionDescription);

    }
    

    private void OnStartButtonPressed()
    {
        var homeUIViewModel = _mainUiService.CurrentUI as HomeUIViewModel;
        homeUIViewModel!.ShowProgressBar(_actionTime, _progressBarPosition, _upgradePoints);
        foreach (var actionReward in _actionRewards)
        {
            if (actionReward.RewardTitle == CharacteristicsType.Stress)
            {
                homeUIViewModel.UpdateStress(actionReward.RewardValue);
            }
        }

        _homeActionProgressHandler.StartActionProgress(_actionTime);
        _popupService.CloseCurrentView();
    }
}
}