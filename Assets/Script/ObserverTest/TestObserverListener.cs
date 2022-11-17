using Script.Libraries.EventSystem;
using Script.Libraries.Observer;

namespace Script.ObserverTest
{
public sealed class TestObserverListener : InGameEventsObserverListenerBase
{
    public override bool EventCondition
    {
        get
        {
            return _modelObservable.Text == "sadas";
        }
    }

    private TestModelObservable _modelObservable;

    public TestObserverListener(IObserver inGameEventsObserver) : base(inGameEventsObserver)
    {
        SubscribeOnEventNotify(Observer);
        UnsubscribeOnNotify(Observer);
    }

    public override void OnEventNotified()
    {
        
    }
}
}