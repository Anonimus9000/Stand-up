using System;

namespace Script.Libraries.Observer.ObservableValue.Base
{
public interface IObservableValue<T>
{
    void Subscribe(Action<T> onNotify);
    void Unsubscribe(Action<T> onNotify);
    void SubscribeWithSingleNotify(Action<T> onNotify);
    void Notify(T value);
}
}