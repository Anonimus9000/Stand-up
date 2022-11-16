using Script.Libraries.EventSystem;
using Script.Libraries.EventSystem.Test;
using Script.Libraries.Observer;

namespace Script.EventSystem.Test
{
    public sealed class TestEventbleController : InGameEventsObservableBase
    {
        private readonly IObserver _observer;
        private TestEventbleModel _testEventbleModel;
        private readonly TestEventableView _view;
        public IConditionEventModel ConditionEventModel => _testEventbleModel;
        public override bool EventCondition => true;

        public TestEventbleController(IObserver observer, IConditionEventModel conditionEventModel, TestEventableView view) : base(observer)
        {
            _view = view;
            _observer = observer;
            _testEventbleModel = (TestEventbleModel)conditionEventModel;
            SubscribeOnEventNotify(_observer);

            _testEventbleModel.TestCondition = true;
        }

        public override void OnEventNotifyd()
        {
            _view.SetText("WORK");
        }
    }
}