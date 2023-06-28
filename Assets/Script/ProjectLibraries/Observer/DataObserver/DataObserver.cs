using System;
using System.Collections.Generic;
using Script.ProjectLibraries.Observer.Base;

namespace Script.ProjectLibraries.Observer.DataObserver
{
public class DataObserver : IObserver
{
    private readonly List<DataObserverListener> _listeners = new(50);

    public void AddListener(IObserverListener observerListener)
    {
        if (observerListener is not DataObserverListener dataObserverListener)
        {
            throw new Exception("Incorrect listener; Need add DataObserverListener");
        }
        
        _listeners.Add(dataObserverListener);
    }

    public void RemoveListener(IObserverListener observerListener)
    {
        if (observerListener is not DataObserverListener dataObserverListener)
        {
            throw new Exception("Incorrect listener; Need add DataObserverListener");
        }
        
        _listeners.Remove(dataObserverListener);
    }

    public void NotifyListeners(INotifyData notifyData)
    {
        foreach (var listener in _listeners)
        {
            listener.OnEventNotified(notifyData);
        }
    }
}
}