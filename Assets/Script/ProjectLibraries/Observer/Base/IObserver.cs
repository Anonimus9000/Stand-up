namespace Script.ProjectLibraries.Observer.Base
{
public interface IObserver
{
    void AddListener(IObserverListener observerListener);
    void RemoveListener(IObserverListener observerListener);
}
}