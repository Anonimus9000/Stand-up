using System;
using UnityEngine;

namespace Script.ConfigData.LocationActionsConfig
{
[Serializable]
public class ActionRewardData
{
    [SerializeField] private float _rewardValue;
    [SerializeField] private CharacteristicsType _rewardTitle;

    public float RewardValue => _rewardValue;
    public CharacteristicsType RewardTitle => _rewardTitle;

    public ActionRewardData(float rewardValue, CharacteristicsType rewardTitle)
    {
        _rewardValue = rewardValue;
        _rewardTitle = rewardTitle;
    }
}
}