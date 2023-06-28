namespace Script.ProjectLibraries.Observer.Base
{
public interface IObserverListener
{
    IObserver Observer { get; }

    void SubscribeOnEventNotify(IObserver observer);
    void UnsubscribeOnNotify(IObserver observer);
}
}