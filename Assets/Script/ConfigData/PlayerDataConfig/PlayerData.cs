using System.Collections.Generic;
using UnityEngine;

namespace Script.ConfigData.PlayerDataConfig
{
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Config/PlayerData", order = 5)] 
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private string _name;
    
    [SerializeField]
    private Texture _avatar;
    
    [SerializeField]
    private List<CharacteristicData> _characteristicsData;

    public string Name => _name;

    public List<CharacteristicData> CharacteristicsData => _characteristicsData;

    public Texture Avatar => _avatar;
}
}