using System;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.Components
{
[Serializable]
public class ActionRewardConstructor
{
    [SerializeField] private float _rewardValue;
    [SerializeField] private CharacteristicsEnum _rewardTitle;

    public float RewardValue => _rewardValue;
    public CharacteristicsEnum RewardTitle => _rewardTitle;

    public ActionRewardConstructor(float rewardValue, CharacteristicsEnum rewardTitle)
    {
        _rewardValue = rewardValue;
        _rewardTitle = rewardTitle;
    }
}
}