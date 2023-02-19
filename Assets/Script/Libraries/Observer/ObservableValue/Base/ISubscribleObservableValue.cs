using System;

namespace Script.Libraries.Observer.ObservableValue.Base
{
public interface ISubscribleObservableValue<T>
{
    T Value { get; }
    void Subscribe(Action<T> onNotify);
    void Unsubscribe(Action<T> onNotify);
    void SubscribeWithSingleNotify(Action<T> onNotify);
}
}