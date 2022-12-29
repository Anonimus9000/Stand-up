using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.PopupDialogs.Components
{
[Serializable]
public class ActionFieldContent
{
    [SerializeField] private string _actionTitle;
    [SerializeField] private Sprite _actionIcon;
    [SerializeField] private float _actionTime;
    [SerializeField] private List<ActionRewardConstructor> _actionRewards;

    public string ActionTitle => _actionTitle;
    public Sprite ActionIcon => _actionIcon;
    public float ActionTime => _actionTime;
    public List<ActionRewardConstructor> ActionRewards => _actionRewards;

    public ActionFieldContent(Sprite actionIcon,string actionTitle, float actionTime, List<ActionRewardConstructor> actionRewards)
    {
        _actionTitle = actionTitle;
        _actionIcon = actionIcon;
        _actionTime = actionTime;
        _actionRewards = actionRewards;
    }
    

}
}