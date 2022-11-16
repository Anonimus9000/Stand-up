namespace Script.Libraries.Observer
{
public interface IObservable
{
    IObserver Observer { get; }
    void OnEventNotifyd();

    void SubscribeOnEventNotify(IObserver observer);
    void UnsubscribeOnNotify(IObserver observer);
}
}