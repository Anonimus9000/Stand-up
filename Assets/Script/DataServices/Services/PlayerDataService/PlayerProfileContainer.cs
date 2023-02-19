using System.Collections.Generic;
using Script.DataServices.Base;
using UnityEngine;

namespace Script.DataServices.Services.PlayerDataService
{
public class PlayerProfileContainer : IDataContainer
{
    public string PlayerName { get; set; }
    public int UpgradePoints { get; set; }
    public CharacteristicModel Charisma { get; set; }
    public CharacteristicModel Insight { get; set; }
    public CharacteristicModel Erudition { get; set; }
    public CharacteristicModel SenceOfHumor { get; set; }
    public CharacteristicModel Appearance { get; set; }
    public CharacteristicModel Stress { get; set; }
    public Texture Avatar { get; set; }
    public List<AchievementData> Achievements { get; set; }
    public List<CharacteristicModel> CharacteristicModels { get; set; }
}
}