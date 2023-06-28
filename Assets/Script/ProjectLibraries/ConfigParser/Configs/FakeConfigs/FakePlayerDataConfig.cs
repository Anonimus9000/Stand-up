using System.Collections.Generic;
using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.PlayerDataData;
using UnityEngine;

namespace Script.ProjectLibraries.ConfigParser.Configs.FakeConfigs
{
public class FakePlayerDataConfig : IPlayerDataConfig
{
    public string Name { get; }
    public List<CharacteristicData> CharacteristicsData { get; }
    public Texture Avatar { get; }
    
    public FakePlayerDataConfig(string name, List<CharacteristicData> characteristicsData, Texture avatar)
    {
        Name = name;
        CharacteristicsData = characteristicsData;
        Avatar = avatar;
    }
}
}