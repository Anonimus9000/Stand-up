using System;
using System.Threading;
using System.Threading.Tasks;
using Script.ProjectLibraries.ConfigParser.Base;
using Script.Scenes.Common.ActionProgressSystem.Handler;
using Script.Utils.ThreadUtils;

namespace Script.Scenes.Home.ActionProgressSystem.Handler
{
public class HomeActionProgressHandler : IActionProgressHandler
{
    public event Action CheckEventSuccessful;
    public event Action ProgressCompleted;
    public event Action ProgressPaused;
    public event Action ProgressСontinued;

    private readonly IInActionProgressConfig _eventsConfig;
    private bool _checkInGameEventsInPause;

    private bool CheckInGameEventsInPause
    {
        get => _checkInGameEventsInPause;
        set
        {
            _checkInGameEventsInPause = value;

            if (_checkInGameEventsInPause)
            {
                ProgressPaused?.Invoke();
            }
            else
            {
                ProgressСontinued?.Invoke();
            }
        }
    }

    private bool _isForceStopped;

    public HomeActionProgressHandler(IInActionProgressConfig eventsConfig)
    {
        _eventsConfig = eventsConfig;
    }

    public void StartActionProgress(float duration)
    {
        _isForceStopped = false;
        
        var tickPerMilliseconds = (int)_eventsConfig.HowOftenTryShowEventPerSecond * 1000;
        var applicationQuitTokenSource = new ApplicationQuitTokenSource();
        
        ProgressTimer(duration, tickPerMilliseconds, applicationQuitTokenSource.Token);
    }

    public void ContinueCheckEvents()
    {
        CheckInGameEventsInPause = false;
    }

    public void ForceStopProgress()
    {
        _isForceStopped = true;
    }

    private async void ProgressTimer(float durationPerSeconds, int tickToTryShowEventPerMilliseconds, CancellationToken token)
    {
        var timeLessMilliseconds = 0;
        var lastCheckTimeMilliseconds = 0;
        var durationInMilliseconds = durationPerSeconds * 1000;

        while (timeLessMilliseconds < durationInMilliseconds)
        {
            token.ThrowIfCancellationRequested();
            
            if(_isForceStopped) return;
            
            await Task.Delay(10, token);

            timeLessMilliseconds += 10;

            if (tickToTryShowEventPerMilliseconds <= timeLessMilliseconds - lastCheckTimeMilliseconds)
            {
                TryShowEvent();
                lastCheckTimeMilliseconds = timeLessMilliseconds;
            }

            while (CheckInGameEventsInPause)
            {
                if(_isForceStopped) return;
                
                await Task.Yield();
            }
        }
        
        if (_isForceStopped) return;
        
        ProgressCompleted?.Invoke();
    }

    private void TryShowEvent()
    {
        var random = new Random();
        var changeToShow = _eventsConfig.ChangeToShowEvent;

        var next = random.Next(0, 99);

        if (changeToShow > next)
        {
            CheckEventSuccessful?.Invoke();
            
            CheckInGameEventsInPause = true;
        }
    }
}
}