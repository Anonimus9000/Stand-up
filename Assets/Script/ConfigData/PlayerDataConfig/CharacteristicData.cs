using System;
using System.Collections.Generic;
using Script.ConfigData.LocationActionsConfig;
using UnityEngine;

namespace Script.ConfigData.PlayerDataConfig
{
[Serializable]
public struct CharacteristicData
{
    [SerializeField]
    private Texture _icon;
    
    [SerializeField]
    private int _currentCharacteristicLevel;

    [SerializeField]
    private int _currentPoints;

    [SerializeField]
    private CharacteristicType _characteristicType;

    [SerializeField]
    private List<CharacteristicLevelData> _levelsData;

    public int CurrentLevel => _currentCharacteristicLevel;
    public int CurrentPoints => _currentPoints;

    public CharacteristicType CharacteristicType => _characteristicType;

    public List<CharacteristicLevelData> LevelsData => _levelsData;
    public Texture Icon => _icon;
}
}