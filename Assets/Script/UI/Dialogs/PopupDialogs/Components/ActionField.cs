using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.Components
{
[Serializable]
public class ActionField
{
    public string ActionTitle;
    public float ActionTime;
    public List<ActionRewardConstructor> ActionRewards;

    public ActionField(string actionTitle, float actionTime, List<ActionRewardConstructor> actionRewards)
    {
        ActionTitle = actionTitle;
        ActionTime = actionTime;
        ActionRewards = actionRewards;
    }
    

}
}