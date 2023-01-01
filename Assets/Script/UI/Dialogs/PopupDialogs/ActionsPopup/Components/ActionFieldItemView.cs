using System.Collections.Generic;
using Script.InteractableObject.ActionProgressSystem;
using Script.Libraries.UISystem.Managers.UiServiceProvider;
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
    private MainUIService _mainUiService;
    private HomeActionProgressHandler _homeActionProgressHandler;
    private float _actionTime;
    private Vector3 _position;

    public void Init(
        Sprite actionIcon,
        string actionTitle,
        List<ActionRewardData> actionDescription,
        float actionTime,
        MainUIService mainUIService,
        HomeActionProgressHandler homeActionProgressHandler,
        Vector3 position)
    {
        _actionDescription.text = string.Empty;
        _actionTitle.text = actionTitle;
        _actionIcon.sprite = actionIcon;
        _startActionButton.onClick.AddListener(OnStartButtonPressed);
        _mainUiService = mainUIService;
        _homeActionProgressHandler = homeActionProgressHandler;
        _actionTime = actionTime;
        _position = position;

        foreach (var actionRewards in actionDescription)
        {
            _actionDescription.text += actionRewards.RewardValue + " " + actionRewards.RewardTitle + "\n";
        }
    }

    private void OnStartButtonPressed()
    {
        var homeUIViewModel = _mainUiService.CurrentUI as HomeUIViewModel;
        _homeActionProgressHandler.StartActionProgress(homeUIViewModel, _actionTime, _position);
    }
}
}