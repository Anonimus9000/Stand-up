using Script.Libraries.EventSystem;
using Script.Libraries.Observer;

namespace Script.ObserverTest
{
public class TestModelObservable : IConditionEventModel, IObserverNotificator
{
    private string _text;
    public IObserver Observer { get; }

    public string Text
    {
        get => _text; 
        set
        {
            _text = value;
            Observer.NotifySubscribers();
        }
    }

    private TestModelObservable(IObserver observer)
    {
        Observer = observer;
    }
}
}