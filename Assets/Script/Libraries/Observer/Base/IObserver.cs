namespace Script.Libraries.Observer.Base
{
public interface IObserver
{
    void AddListener(IObserverListener observerListener);
    void RemoveListener(IObserverListener observerListener);
}
}