using System;
using System.Collections.Generic;
using Script.ProjectLibraries.Observer.ObservableValue.Base;

namespace Script.ProjectLibraries.Observer.ObservableValue
{
public class NotifyObservableValue<TValue> : INotifyObservableValue<TValue>
{
    public TValue Value => _value;
    
    private readonly List<Subscriber<TValue>> _subscribers;
    private TValue _value;

    public NotifyObservableValue(TValue value, int subscribersCapacity = 5)
    {
        _value = value;
        
        _subscribers = new List<Subscriber<TValue>>(subscribersCapacity);
    }

    public void Subscribe(Action<TValue> onNotify)
    {
        var subscriber = new Subscriber<TValue>(onNotify, false);
        _subscribers.Add(subscriber);
    }

    public void Unsubscribe(Action<TValue> onNotify)
    {
        for (var index = 0; index < _subscribers.Count; index++)
        {
            if (_subscribers[index].OnNotify != onNotify) continue;
            
            _subscribers.Remove(_subscribers[index]);
            
            break;
        }
    }

    public void SubscribeWithSingleNotify(Action<TValue> onNotify)
    {
        var subscriber = new Subscriber<TValue>(onNotify, true);
        _subscribers.Add(subscriber);
    }

    public void Notify(TValue value)
    {
        _value = value;
        
        foreach (var subscriber in _subscribers)
        {
            subscriber.OnNotify?.Invoke(value);
        }
    }
}
}