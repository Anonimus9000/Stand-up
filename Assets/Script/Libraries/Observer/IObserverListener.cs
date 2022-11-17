namespace Script.Libraries.Observer
{
public interface IObserverListener
{
    IObserver Observer { get; }
    void OnEventNotified();

    void SubscribeOnEventNotify(IObserver observer);
    void UnsubscribeOnNotify(IObserver observer);
}
}