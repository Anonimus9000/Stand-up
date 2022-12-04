using Script.Libraries.Observer.Base;

namespace Script.Libraries.Observer.InGameEventSystem
{
public abstract class InGameEventsObserverListenerBase : IObserverListener
{
    public IObserver Observer { get; }

    protected InGameEventsObserverListenerBase(IObserver inGameEventsObserver)
    {
        Observer = inGameEventsObserver;
    }

    #region AbstractMethods

    public abstract bool EventCondition();

    public abstract void OnEventNotified();

    #endregion

    #region VirtualMethods

    public virtual void SubscribeOnEventNotify(IObserver observer)
    {
        Observer.AddListener(this);
    }

    public virtual void UnsubscribeOnNotify(IObserver observer)
    {
        Observer.RemoveListener(this);
    }

    #endregion
}
}