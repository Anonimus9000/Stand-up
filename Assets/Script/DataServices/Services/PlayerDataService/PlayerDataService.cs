using System;
using System.Collections.Generic;
using Script.DataServices.Base;
using Script.DataServices.DataLoader;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.LocationActionsData;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.PlayerDataData;
using UnityEngine;

namespace Script.DataServices.Services.PlayerDataService
{
public class PlayerDataService : IDataService
{
    public IDataContainer DataContainer => _containerModel;
    
    public event Action<string> NameChanged;
    public event Action<int> UpgradePointsChanged;
    public event Action<Texture> AvatarChanged;
    public event Action<List<AchievementData>> AchievementsChanged;

    private readonly PlayerDataLoader _playerDataLoader;
    private readonly PlayerProfileContainer _containerModel;

    public PlayerDataService(IDataLoader playerDataLoader)
    {
        _playerDataLoader = playerDataLoader as PlayerDataLoader;
        _containerModel = new PlayerProfileContainer();

        InitData();
    }

    public void AddCharisma(int charismaPoints)
    {
        _containerModel.Charisma.AddPoints(charismaPoints);
    }

    public void UpdateName(string newPlayerName)
    {
        _containerModel.PlayerName = newPlayerName;

        NameChanged?.Invoke(newPlayerName);
    }

    public void AddUpgradePoints(int upgradePoints)
    {
        _containerModel.UpgradePoints += upgradePoints;

        UpgradePointsChanged?.Invoke(_containerModel.UpgradePoints);
    }

    public void AddInsight(int insightPoints)
    {
        _containerModel.Insight.AddPoints(insightPoints);
    }

    public void AddErudition(int eruditionPoints)
    {
        _containerModel.Erudition.AddPoints(eruditionPoints);
    }

    public void AddSenceOfHumor(int senceOfHumorPoints)
    {
        _containerModel.SenceOfHumor.AddPoints(senceOfHumorPoints);
    }

    public void AddAppearance(int appearancePoints)
    {
        _containerModel.Appearance.AddPoints(appearancePoints);
    }

    public void AddStress(int stressPoints)
    {
        _containerModel.Stress.AddPoints(stressPoints);
    }

    public void UpdateAvatar(Texture avatar)
    {
        _containerModel.Avatar = avatar;

        AvatarChanged?.Invoke(avatar);
    }

    public void AddAchievements(AchievementData achievement)
    {
        _containerModel.Achievements.Add(achievement);

        AchievementsChanged?.Invoke(_containerModel.Achievements);
    }

    private void InitData()
    {
        _containerModel.PlayerName = _playerDataLoader.PlayerDataFakeConfig.Name;
        _containerModel.Avatar = _playerDataLoader.PlayerDataFakeConfig.Avatar;
        var characteristicsData = _playerDataLoader.PlayerDataFakeConfig.CharacteristicsData;

        foreach (var characteristicData in characteristicsData)
        {
            switch (characteristicData.CharacteristicType)
            {
                case CharacteristicType.Erudition:
                    _containerModel.Erudition = ParseCharacteristic(characteristicData);
                    break;
                case CharacteristicType.Insight:
                    _containerModel.Insight = ParseCharacteristic(characteristicData);
                    break;
                case CharacteristicType.SenceOfHumor:
                    _containerModel.SenceOfHumor = ParseCharacteristic(characteristicData);
                    break;
                case CharacteristicType.Charisma:
                    _containerModel.Charisma = ParseCharacteristic(characteristicData);
                    break;
                case CharacteristicType.Appearance:
                    _containerModel.Appearance = ParseCharacteristic(characteristicData);
                    break;
                case CharacteristicType.Stress:
                    _containerModel.Stress = ParseCharacteristic(characteristicData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private CharacteristicModel ParseCharacteristic(CharacteristicData characteristicData)
    {
        var levelToLimitPoints = new Dictionary<int, int>(characteristicData.LevelsData.Count);
        
        foreach (var levelsData in characteristicData.LevelsData)
        {
            levelToLimitPoints[levelsData.Level] = levelsData.PointsToNextLevel;
        }
        
        var characteristic = new CharacteristicModel(
            characteristicData.CurrentLevel,
            characteristicData.CurrentPoints,
            characteristicData.CharacteristicType.ToString(),
            characteristicData.Icon,
            levelToLimitPoints);

        return characteristic;
    }
}
}