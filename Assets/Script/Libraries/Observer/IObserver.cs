namespace Script.Libraries.Observer
{
public interface IObserver
{
    void AddEvent(IObserverListener observerListener);
    void RemoveEvent(IObserverListener observerListener);
    void NotifySubscribers();
}
}