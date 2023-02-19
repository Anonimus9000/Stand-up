using System;
using UnityEngine;

namespace Script.ConfigData.LocationActionsConfig
{
[Serializable]
public class ActionRewardData
{
    [SerializeField] private float _rewardValue;
    [SerializeField] private CharacteristicType _rewardTitle;

    public float RewardValue => _rewardValue;
    public CharacteristicType RewardTitle => _rewardTitle;

    public ActionRewardData(float rewardValue, CharacteristicType rewardTitle)
    {
        _rewardValue = rewardValue;
        _rewardTitle = rewardTitle;
    }
}
}