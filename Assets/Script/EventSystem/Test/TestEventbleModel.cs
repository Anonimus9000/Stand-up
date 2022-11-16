using Script.Libraries.Observer;

namespace Script.Libraries.EventSystem.Test
{
    public class TestEventbleModel : IObserverNotificator, IConditionEventModel
    {
        public TestEventbleModel(IObserver observer)
        {
            Observer = observer;
        }
        public IObserver Observer { get; }

        public bool TestCondition
        {
            get => _testCondition;
            set
            {
                _testCondition = value;
                
                Observer.NotifySubscribers();
            }
        }

        private bool _testCondition;
    }
}