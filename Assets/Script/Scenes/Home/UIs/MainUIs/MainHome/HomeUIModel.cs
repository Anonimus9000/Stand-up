using System;
using System.Threading;
using System.Threading.Tasks;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.Observer.ObservableValue;
using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.Scenes.Common.ActionProgressSystem.Handler;
using Script.Scenes.Home.ActionProgressSystem.Handler;
using Script.Scenes.Home.UIs.MainUIs.MainHome.Components;
using Script.UI.CommonUIs.PopupDialogs.InGameEvent;
using Script.Utils.ThreadUtils;
using Script.Utils.UIPositionConverter;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Script.Scenes.Home.UIs.MainUIs.MainHome
{
//TODO: progress bar work only with one progress bar. Make it work with many progress bar
public class HomeUIModel : Model
{
    public event Action<float> MoveBubbleCompleted;

    public NotifyObservableValue<int> UpgradePoints { get; } = new(0, 5);
    
    private readonly PositionsConverter _positionConverter;
    private readonly IUIService _popupsUIService;
    private readonly HomeActionProgressHandler _homeActionProgressHandler;
    private readonly Canvas _canvas;
    private readonly Camera _mainCamera;
    private ProgressBar _progressBar;
    private bool _progressIsPause;

    public HomeUIModel(
        PositionsConverter positionsConverter, 
        IUIService popupsUIService,
        HomeActionProgressHandler homeActionProgressHandler,
        Canvas canvas,
        Camera mainCamera)
    {
        _popupsUIService = popupsUIService;
        _homeActionProgressHandler = homeActionProgressHandler;
        _canvas = canvas;
        _mainCamera = mainCamera;
        _positionConverter = positionsConverter;
    }

    private Vector3 GetScreenPointPositionByWorld(Vector3 worldPosition)
    {
        var worldToScreenSpace = _positionConverter.WorldToScreenSpace(worldPosition);

        return worldToScreenSpace;
    }
    
    #region ActionProgress


    public void UpdateStress(int stressPoint)
    {
        if (stressPoint < 0)
        {
            throw new Exception("Points can't be less zero");
        }
        
    }

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
        var progressBarPosition = GetScreenPointPositionByWorld(worldPosition);

        if (_progressBar == null)
        {
            _progressBar = Object.Instantiate(prefab, progressBarParent);
        }
        else
        {
            _progressBar.gameObject.SetActive(true);
        }

        SubscribeOnProgressEvents(progressHandler, _progressBar);
        
        _progressBar.transform.localPosition = progressBarPosition;
        
        _progressBar.ShowProgress(duration);
        
        progressHandler.StartActionProgress(duration);

        var applicationQuitTokenSource = new ApplicationQuitTokenSource();
        StartShowBubbles(duration,
            points,
            flyBubblePrefab,
            bubblesParent,
            upgradePointsIcon,
            progressBarPosition,
            applicationQuitTokenSource.Token);

        return _progressBar;
    }

    private void SubscribeOnProgressEvents(HomeActionProgressHandler homeActionProgressHandler, ProgressBar progressBar)
    {
        homeActionProgressHandler.CheckEventSuccessful += OnCheckEventSuccessful;
        homeActionProgressHandler.ProgressPaused += OnProgressPaused;
        homeActionProgressHandler.ProgressСontinued += OnProgressContinued;
        homeActionProgressHandler.ProgressCompleted += OnProgressCompleted;
        progressBar.ProgressAnimationCompleted += OnProgressBarAnimationCompleted;

    }

    private void OnProgressBarAnimationCompleted()
    {
        _progressBar.gameObject.SetActive(false);
    }

    private void OnProgressContinued()
    {
        _progressBar.Continue();
        _progressIsPause = false;
    }

    private void OnProgressPaused()
    {
        _progressBar.Pause();
        _progressIsPause = true;
    }

    private void OnProgressCompleted()
    {
        UnsubscribeOnProgressEvents(_homeActionProgressHandler);
    }

    private void UnsubscribeOnProgressEvents(HomeActionProgressHandler homeActionProgressHandler)
    {
        homeActionProgressHandler.CheckEventSuccessful -= OnCheckEventSuccessful;
        homeActionProgressHandler.ProgressPaused -= OnCheckEventSuccessful;
        homeActionProgressHandler.ProgressСontinued -= OnCheckEventSuccessful;
        homeActionProgressHandler.ProgressCompleted -= OnProgressCompleted;
    }

    private void OnCheckEventSuccessful()
    {
        var inGameViewModel = AddDisposable(new InGameEventViewModel(_popupsUIService));
        _popupsUIService.Show<InGameEventView>(inGameViewModel);

        inGameViewModel.EventCompleted += OnInGameEventCompleted;
    }

    private void OnInGameEventCompleted()
    {
        _homeActionProgressHandler.ContinueCheckEvents();
    }


    private async void StartShowBubbles(
        float duration,
        int allPoints,
        FlyBubble flyBubblePrefab,
        Transform bubblesParent,
        Transform upgradePointsIcon,
        Vector3 startMoveBubblePosition,
        CancellationToken token)
    {
        var lessTime = duration;
        var checkPeriod = 1;
        var pointsPerPeriod = (int) (allPoints / duration);

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

            if (_progressIsPause)
            {
                await Task.Yield();
                continue;
            }
            
            //WorldToCanvasPoint(_canvas, _mainCamera, startMoveBubble.position, out var startMoveRectPosition);
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(
            //     _canvas, 
            //     startMoveBubble.position,
            //     _mainCamera, 
            //     out var startMovePositionRect);

            await Task.Delay(checkPeriod * 1000, token);
            lessTime -= checkPeriod;
            
            ShowBubble(startMoveBubblePosition, pointsPerPeriod, flyBubblePrefab, bubblesParent, upgradePointsIcon);
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
    
    private void WorldToCanvasPoint(Canvas canvas, Camera camera, Vector3 worldPosition, out Vector2 canvasPosition)
    {
        var screenPoint = camera.WorldToScreenPoint(worldPosition);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPoint, null, out Vector2 canvasPoint);

        canvasPosition =  canvasPoint;
    }
    
    #endregion
}
}