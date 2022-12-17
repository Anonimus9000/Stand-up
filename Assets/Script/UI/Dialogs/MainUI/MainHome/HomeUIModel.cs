using System;
using Script.Libraries.MVVM;

namespace Script.UI.Dialogs.MainUI.MainHome
{
public class HomeUIModel : IModel
{
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