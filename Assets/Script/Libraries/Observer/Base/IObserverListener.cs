namespace Script.Libraries.Observer.Base
{
public interface IObserverListener
{
    IObserver Observer { get; }
    void OnEventNotified();
    public bool EventCondition();

    void SubscribeOnEventNotify(IObserver observer);
    void UnsubscribeOnNotify(IObserver observer);
}
}