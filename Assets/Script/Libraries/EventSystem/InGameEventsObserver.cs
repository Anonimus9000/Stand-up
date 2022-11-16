using System;
using System.Collections.Generic;
using Script.Libraries.Observer;

namespace Script.Libraries.EventSystem
{
public class InGameEventsObserver : IObserver
{
    private readonly Dictionary<InGameEventsObservableBase, Action> _subscribers;

    public InGameEventsObserver()
    {
        _subscribers = new Dictionary<InGameEventsObservableBase, Action>();
    }

    public void AddEvent(IObservable observable)
    {
        if (observable is InGameEventsObservableBase inGameEventsObserver)
        {
            _subscribers.Add(inGameEventsObserver, observable.OnEventNotifyd);
        }
        else
        {
            throw new Exception($"Incorrect type {observable.GetType()}");
        }
    }

    public void RemoveEvent(IObservable observable)
    {
        if (observable is InGameEventsObservableBase inGameEventsObserver)
        {
            _subscribers.Remove(inGameEventsObserver);
        }
        else
        {
            throw new Exception($"Incorrect type {observable.GetType()}");
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