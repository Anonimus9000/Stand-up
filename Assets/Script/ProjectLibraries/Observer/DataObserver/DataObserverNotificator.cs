using System;
using Script.ProjectLibraries.Observer.Base;

namespace Script.ProjectLibraries.Observer.DataObserver
{
public abstract class DataObserverNotificator : IObserverNotificator
{
    public IObserver Observer
    {
        get => _observer;
        private set => _observer = (DataObserver)value;
    }

    private DataObserver _observer;

    private DataObserverNotificator(IObserver observer)
    {
        if (observer is not DataObserver dataObserver)
        {
            throw new Exception("Incorrect observer; Need use DataObserver");
        }
        
        Observer = dataObserver;
    }

    protected virtual void NotifyListener(INotifyData data)
    {
        _observer.NotifyListeners(data);
    }
}
}