using System;
using System.Threading;
using System.Threading.Tasks;
using Script.ConfigData.InGameEventsConfig;
using Script.Initializer;
using Script.Utils.ThreadUtils;

namespace Script.InteractableObject.ActionProgressSystem.Handler
{
public class HomeActionProgressHandler : IInitializable, IActionProgressHandler
{
    public event Action TryShowEventSuccessful;
    
    private readonly InActionProgressConfig _config;
    private bool _eventViewShown;

    public HomeActionProgressHandler(InActionProgressConfig config)
    {
        _config = config;
    }

    public void StartActionProgress(float duration)
    {
        var tickPerMilliseconds = (int)_config.HowOftenTryShowEventPerSecond * 1000;
        var applicationQuitTokenSource = new ApplicationQuitTokenSource();
        
        ProgressTimer(duration, tickPerMilliseconds, applicationQuitTokenSource.Token);
    }

    private async void ProgressTimer(float durationPerSeconds, int tickToTryShowEventPerMilliseconds, CancellationToken token)
    {
        var timeLess = 0f;

        while (timeLess < durationPerSeconds)
        {
            await Task.Delay(tickToTryShowEventPerMilliseconds, token);

            timeLess += tickToTryShowEventPerMilliseconds / 1000f;
            
            if (TryShowEvent())
            {
                while (_eventViewShown)
                {
                    await Task.Yield();
                }
            }
        }
    }

    private bool TryShowEvent()
    {
        var random = new Random();
        var changeToShow = _config.ChangeToShowEvent;

        var next = random.Next(0, 99);

        if (changeToShow > next)
        {
            TryShowEventSuccessful?.Invoke();
            
            _eventViewShown = true;
            
            return true;
        }

        return false;
    }
}
}