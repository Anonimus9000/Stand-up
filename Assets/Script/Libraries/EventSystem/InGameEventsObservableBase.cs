using Script.Libraries.Observer;

namespace Script.Libraries.EventSystem
{
public abstract class InGameEventsObservableBase : IObservable
{
    public IObserver Observer { get; }

    public abstract bool EventCondition { get; }

    protected InGameEventsObservableBase(IObserver inGameEventsObserver)
    {
        Observer = inGameEventsObserver;
    }

    public abstract void OnEventNotifyd();

    public virtual void SubscribeOnEventNotify(IObserver observer)
    {
        Observer.AddEvent(this);
    }

    public virtual void UnsubscribeOnNotify(IObserver observer)
    {
        Observer.RemoveEvent(this);
    }
}
}