using System;
using Script.Libraries.MVVM;
using Script.UI.Converter;
using UnityEngine;

namespace Script.UI.Dialogs.MainUI.MainHome
{
public class HomeUIModel : IModel
{
    private readonly PositionsConverter _positionConverter;

    public HomeUIModel(PositionsConverter positionsConverter)
    {
        _positionConverter = positionsConverter;
    }

    public Vector3 GetScreenPointPositionByWorld(Vector3 worldPosition, RectTransform area)
    {
        var worldToScreenSpace = _positionConverter.WorldToScreenSpace(worldPosition, area);

        return worldPosition;
    }
    
    #region ProgressBar
    

    #endregion
    #region UpgradePoints

    public event Action<int> UpgradePointsChanged;

    private int _upgradePoints;

    public void InitUpgradePoints(int upgradePoints)
    {
        if (upgradePoints < 0)
        {
            throw new Exception("Points can't be less zero");
        }
        
        _upgradePoints = upgradePoints;
    }

    public void UpdateUpgradePoints(int pointsDifference)
    {
        _upgradePoints += pointsDifference;
        UpgradePointsChanged?.Invoke(_upgradePoints);
    }

    #endregion
}
}