namespace Script.ProjectLibraries.Observer.ObservableValue.Base
{
public interface INotifyObservableValue<T> : ISubscribleObservableValue<T>
{
    void Notify(T value);
}
}