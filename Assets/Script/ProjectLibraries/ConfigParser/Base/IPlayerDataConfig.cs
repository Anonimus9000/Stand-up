using System.Collections.Generic;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.PlayerDataData;
using UnityEngine;

namespace Script.ProjectLibraries.ConfigParser.Base
{
public interface IPlayerDataConfig : IConfig
{
    public string Name { get; }
    public List<CharacteristicData> CharacteristicsData { get;}
    public Texture Avatar { get; }
}
}