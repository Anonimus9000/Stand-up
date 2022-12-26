using System;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.Components
{
[Serializable]
public class ActionRewardConstructor
{
    public float RewardValue;
    public CharacteristicsEnum RewardTitle;

    public ActionRewardConstructor(float rewardValue, CharacteristicsEnum rewardTitle)
    {
        RewardValue = rewardValue;
        RewardTitle = rewardTitle;
    }
}
}