using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.ActionsPopup.Components
{
[Serializable]
public class ActionFieldData
{
    [SerializeField]
    private string _actionTitle;

    [SerializeField]
    private Sprite _actionIcon;

    [SerializeField]
    private float _actionTime;

    [SerializeField]
    private List<ActionRewardData> _actionRewards;

    [SerializeField]
    private int _upgradePoints = 30;

    public string ActionTitle => _actionTitle;
    public Sprite ActionIcon => _actionIcon;
    public float ActionTime => _actionTime;
    public List<ActionRewardData> ActionRewards => _actionRewards;
    public int UpgradePoints => _upgradePoints;

    public ActionFieldData(Sprite actionIcon, string actionTitle, float actionTime,
        List<ActionRewardData> actionRewards)
    {
        _actionTitle = actionTitle;
        _actionIcon = actionIcon;
        _actionTime = actionTime;
        _actionRewards = actionRewards;
    }
}
}