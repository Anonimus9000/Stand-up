using System;
using Script.DataServices.Services.PlayerDataService;
using Script.ProjectLibraries.MVVM;
using UnityEngine;

namespace Script.UI.CommonUIs.FullscreenDialogs.CharacterInfo.Characteristics
{
public class CharacteristicElementModel : Model
{
    public event Action<int> PercentProgressChanged;
    public event Action<int> LevelChanged;
    public event Action<string> NameChanged;
    public event Action<Texture> IconChanged;

    public int Level { get; }
    public string Name { get; }
    public Texture Icon { get; }
    public int PointsToNextLevel { get; private set; }
    public int Points { get; }

    public CharacteristicElementModel(CharacteristicModel characteristicModel)
    {
        Level = characteristicModel.Level;
        Name = characteristicModel.CharacteristicsName;
        Icon = characteristicModel.Icon;
        PointsToNextLevel = characteristicModel.PointsToNextLevel;
        Points = characteristicModel.Points;
        
        InitCharacteristicData(
            Name,
            Level,
            PointsToNextLevel,
            Points, 
            Icon);
    }
    
    private void InitCharacteristicData(string name, int level, int pointsToNextLevel, int points, Texture icon)
    {
        NameChanged?.Invoke(name);
        LevelChanged?.Invoke(level);
        IconChanged?.Invoke(icon);
        
        PointsToNextLevel = pointsToNextLevel;
    }

    public void UpdateProgress()
    {
        InitProgress(PointsToNextLevel, Points);
    }

    private void InitProgress(int pointsToNextLevel, int currentPoints)
    {
        var percentCompletedToNextLvl = (float)currentPoints / (float)pointsToNextLevel * 100;
        
        PercentProgressChanged?.Invoke((int)percentCompletedToNextLvl);
    }
}
}