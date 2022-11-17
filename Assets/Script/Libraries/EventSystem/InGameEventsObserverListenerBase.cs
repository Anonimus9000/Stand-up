using Script.Libraries.Observer;

namespace Script.Libraries.EventSystem
{
public abstract class InGameEventsObserverListenerBase : IObserverListener
{
    public IObserver Observer { get; }

    public abstract bool EventCondition { get; }

    protected InGameEventsObserverListenerBase(IObserver inGameEventsObserver)
    {
        Observer = inGameEventsObserver;
    }

    public abstract void OnEventNotified();

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