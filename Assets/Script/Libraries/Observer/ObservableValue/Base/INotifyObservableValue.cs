namespace Script.Libraries.Observer.ObservableValue.Base
{
public interface INotifyObservableValue<T> : ISubscribleObservableValue<T>
{
    void Notify(T value);
}
}