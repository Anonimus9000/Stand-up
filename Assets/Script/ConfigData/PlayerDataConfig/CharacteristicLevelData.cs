using System;
using UnityEngine;

namespace Script.ConfigData.PlayerDataConfig
{
[Serializable]
public struct CharacteristicLevelData
{
    [SerializeField]
    private int _level;

    [SerializeField]
    private int _pointsToNextLevel;

    public int Level => _level;
    public int PointsToNextLevel => _pointsToNextLevel;
}
}