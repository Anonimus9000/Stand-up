using System.Collections.Generic;
using UnityEngine;

namespace Script.DataServices.Services.PlayerDataService
{
public class CharacteristicModel
{
    public Texture Icon { get; }
    public string CharacteristicsName { get; }
    public int Level { get; private set; }
    public int Points { get; private set; }
    public int PointsToNextLevel => _levelToLimitPoints[Level];
    private readonly Dictionary<int, int> _levelToLimitPoints;

    public CharacteristicModel(int level, int points, string characteristicsName, Texture icon, Dictionary<int, int> levelToLimitPoints)
    {
        Icon = icon;
        Level = level;
        Points = points;
        CharacteristicsName = characteristicsName;
        _levelToLimitPoints = levelToLimitPoints;
    }

    public void AddPoints(int points)
    {
        var pointsToNextLevel = _levelToLimitPoints[Level];

        if (pointsToNextLevel < Points + points)
        {
            Level++;
            var remainingPoints  = Points + points - pointsToNextLevel;
            Points = remainingPoints;
        }
        else
        {
            Points += points;
        }
    }
}
}