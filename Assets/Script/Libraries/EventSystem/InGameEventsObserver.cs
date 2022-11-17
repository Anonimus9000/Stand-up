using System;
using System.Collections.Generic;
using Script.Libraries.Observer;

namespace Script.Libraries.EventSystem
{
public class InGameEventsObserver : IObserver
{
    private readonly Dictionary<InGameEventsObserverListenerBase, Action> _subscribers;

    public InGameEventsObserver()
    {
        _subscribers = new Dictionary<InGameEventsObserverListenerBase, Action>();
    }

    public void AddEvent(IObserverListener observerListener)
    {
        if (observerListener is InGameEventsObserverListenerBase inGameEventsObserver)
        {
            _subscribers.Add(inGameEventsObserver, observerListener.OnEventNotified);
        }
        else
        {
            throw new Exception($"Incorrect type {observerListener.GetType()}");
        }
    }

    public void RemoveEvent(IObserverListener observerListener)
    {
        if (observerListener is InGameEventsObserverListenerBase inGameEventsObserver)
        {
            _subscribers.Remove(inGameEventsObserver);
        }
        else
        {
            throw new Exception($"Incorrect type {observerListener.GetType()}");
        }
    }

    public void NotifySubscribers()
    {
        foreach (var observableKey in _subscribers.Keys)
        {
            if (observableKey.EventCondition)
            {
                _subscribers[observableKey]?.Invoke();
            }
        }
    }
}
}