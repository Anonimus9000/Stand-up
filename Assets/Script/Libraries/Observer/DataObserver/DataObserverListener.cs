using Script.Libraries.Observer.Base;

namespace Script.Libraries.Observer.DataObserver
{
public abstract class DataObserverListener : IObserverListener
{
    public IObserver Observer { get; }

    protected DataObserverListener(IObserver observer)
    {
        Observer = observer;
    }

    #region AbstractMethods

    public abstract void OnEventNotified(INotifyData notifyData);

    #endregion

    #region VirtualMethods

    public virtual void SubscribeOnEventNotify(IObserver observer)
    {
        observer.AddListener(this);
    }

    public virtual void UnsubscribeOnNotify(IObserver observer)
    {
        observer.RemoveListener(this);
    }

    #endregion
}
}