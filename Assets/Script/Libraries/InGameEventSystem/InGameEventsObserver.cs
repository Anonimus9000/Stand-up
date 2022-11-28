using System;
using System.Collections.Generic;
using Script.Libraries.Observer;
using Script.Libraries.Observer.Base;

namespace Script.Libraries.InGameEventSystem
{
public class InGameEventsObserver : IObserver
{
    private readonly Dictionary<InGameEventsObserverListenerBase, Action> _listenersEventPairs;

    public InGameEventsObserver()
    {
        _listenersEventPairs = new Dictionary<InGameEventsObserverListenerBase, Action>();
    }

    public void AddEvent(IObserverListener observerListener)
    {
        if (observerListener is InGameEventsObserverListenerBase inGameEventsObserver)
        {
            _listenersEventPairs.Add(inGameEventsObserver, observerListener.OnEventNotified);
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
            _listenersEventPairs.Remove(inGameEventsObserver);
        }
        else
        {
            throw new Exception($"Incorrect type {observerListener.GetType()}");
        }
    }

    public void NotifySubscribers()
    {
        foreach (var observableKey in _listenersEventPairs.Keys)
        {
            var isConditionSuccess = observableKey.EventCondition();
            
            if (isConditionSuccess)
            {
                _listenersEventPairs[observableKey]?.Invoke();
            }
        }
    }
}
}