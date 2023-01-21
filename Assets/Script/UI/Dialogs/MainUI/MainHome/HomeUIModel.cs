using System;
using System.Threading;
using System.Threading.Tasks;
using Script.Initializer.StartApplicationDependenciesInitializers;
using Script.InteractableObject.ActionProgressSystem.Handler;
using Script.Libraries.MVVM;
using Script.Libraries.Observer.ObservableValue;
using Script.UI.Converter;
using Script.UI.Dialogs.MainUI.MainHome.Components;
using Script.Utils.ThreadUtils;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Script.UI.Dialogs.MainUI.MainHome
{
public class HomeUIModel : IModel
{
    private readonly PositionsConverter _positionConverter;
    private Transform _upgradePointsIcon;
    private Transform _bubblesParent;
    private FlyBubble _flyBubblePrefab;

    public HomeUIModel(PositionsConverter positionsConverter)
    {
        _positionConverter = positionsConverter;
    }

    private Vector3 GetScreenPointPositionByWorld(Vector3 worldPosition)
    {
        var worldToScreenSpace = _positionConverter.WorldToScreenSpace(worldPosition);

        return worldToScreenSpace;
    }
    
    #region ActionProgress

    public event Action<float> MoveBubbleCompleted;

    public ObservableValue<int> UpgradePoints { get; } = new(0, 5);

    public void InitUpgradePoints(int upgradePoints)
    {
        if (upgradePoints < 0)
        {
            throw new Exception("Points can't be less zero");
        }
        
        UpgradePoints.Notify(upgradePoints);
    }

    public ProgressBar ShowProgress(
        int points,
        float duration,
        Vector3 worldPosition,
        ProgressBar prefab,
        Transform progressBarParent,
        FlyBubble flyBubblePrefab,
        Transform bubblesParent,
        Transform upgradePointsIcon,
        HomeActionProgressHandler progressHandler)
    {
        _flyBubblePrefab = flyBubblePrefab;
        _bubblesParent = bubblesParent;
        _upgradePointsIcon = upgradePointsIcon;
        
        var progressBarPosition = GetScreenPointPositionByWorld(worldPosition);
        
        var progressBar = Object.Instantiate(prefab, progressBarParent);
        progressBar.transform.localPosition = progressBarPosition;
        
        progressBar.ShowProgress(duration);
        
        progressHandler.StartActionProgress(duration);

        var applicationQuitTokenSource = new ApplicationQuitTokenSource();
        StartShowBubbles(duration,
            points,
            flyBubblePrefab,
            bubblesParent,
            upgradePointsIcon,
            progressBar.transform,
            applicationQuitTokenSource.Token);

        return progressBar;
    }

    private async void StartShowBubbles(
        float duration,
        int allPoints,
        FlyBubble flyBubblePrefab,
        Transform bubblesParent,
        Transform upgradePointsIcon,
        Transform startMoveBubble,
        CancellationToken token)
    {
        var pointsToGet = allPoints;
        var lessTime = duration;
        var checkPeriod = 1;
        var pointsPerPeriod = (int) (allPoints / duration);
        var random = new Random();
        
        Debug.Log($"Start show bubbles; duration = {duration}");
        Debug.Log($"Points = {pointsToGet}");

        while (true)
        {
            if (lessTime <= 0)
            {
                return;
            }
            
            if (token.IsCancellationRequested || upgradePointsIcon == null)
            {
                return;
            }
            
            ShowBubble(startMoveBubble.position, pointsPerPeriod, flyBubblePrefab, bubblesParent, upgradePointsIcon);
            
            await Task.Delay(checkPeriod * 1000, token);
            lessTime -= checkPeriod;
        }
    }

    private void ShowBubble(
        Vector3 startMovePosition,
        int upgradePointsDifference,
        FlyBubble flyBubblePrefab,
        Transform bubblesParent,
        Transform upgradePointsIcon)
    {
        var flyBubble = Object.Instantiate(flyBubblePrefab, bubblesParent);
        var upgradePointsLocalPosition = upgradePointsIcon.position;

        flyBubble.ShowAndMoveBubble(startMovePosition, upgradePointsLocalPosition, upgradePointsDifference);

        flyBubble.MoveCompleted += OnMoveBubbleCompleted;
    }

    private void OnMoveBubbleCompleted(FlyBubble bubble)
    {
        var currentPoints = UpgradePoints.Value + bubble.BodyInfo;
        UpgradePoints.Notify(currentPoints);
        
        bubble.Destroy();
        bubble.MoveCompleted -= OnMoveBubbleCompleted;
    }
    #endregion
}
}