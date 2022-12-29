using System;
using System.Collections.Generic;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.UI.Dialogs.MainUI.MainHome;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using NotImplementedException = System.NotImplementedException;

namespace Script.UI.Dialogs.PopupDialogs.Components
{
public class ActionFields: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _actionTitle;
    [SerializeField] private TextMeshProUGUI _actionDescription;
    [SerializeField] private Image _actionIcon;
    [SerializeField] public Button StartActionButton;
    
    private HomeUIViewModel _homeUIViewModel;
    private MainUIService _mainUiService;
    private ActionProgressHandler _actionProgressHandler;
    private float _actionTime;
    private Vector3 _position;
    

    public void Init(
        Sprite actionIcon,
        string actionTitle,
        List<ActionRewardConstructor> actionDescription,
        float actionTime,
        MainUIService mainUIService,
        ActionProgressHandler actionProgressHandler,
        Vector3 position)
    {
        _actionDescription.text = string.Empty;
        _actionTitle.text = actionTitle;
        _actionIcon.sprite = actionIcon;
        StartActionButton.onClick.AddListener(OnStartButtonPressed);
        _mainUiService = mainUIService;
        _actionProgressHandler = actionProgressHandler;
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
        _actionProgressHandler.StartActionProgress(homeUIViewModel, _actionTime, _position);
    }
}
}