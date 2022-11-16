namespace Script.Libraries.Observer
{
public interface IObserver
{
    void AddEvent(IObservable observable);
    void RemoveEvent(IObservable observable);
    void NotifySubscribers();
}
}