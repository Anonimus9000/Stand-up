namespace Script.Libraries.Observer.Base
{
public interface IObserver
{
    void AddEvent(IObserverListener observerListener);
    void RemoveEvent(IObserverListener observerListener);
    void NotifySubscribers();
}
}