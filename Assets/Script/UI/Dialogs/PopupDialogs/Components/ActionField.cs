using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.PopupDialogs.Components
{
[Serializable]
public class ActionField
{
    public string ActionTitle;
    public Sprite ActionIcon;
    public float ActionTime;
    public List<ActionRewardConstructor> ActionRewards;

    public ActionField(Sprite actionIcon,string actionTitle, float actionTime, List<ActionRewardConstructor> actionRewards)
    {
        ActionTitle = actionTitle;
        ActionIcon = actionIcon;
        ActionTime = actionTime;
        ActionRewards = actionRewards;
    }
    

}
}