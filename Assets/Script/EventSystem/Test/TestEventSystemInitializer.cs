using Script.EventSystem.Test;
using UnityEngine;

namespace Script.Libraries.EventSystem.Test
{
    public class TestEventSystemInitializer : MonoBehaviour
    {
        [SerializeField] private TestEventableView _view;
        private InGameEventsObserver _inGameEventsObserver;

        private void Awake()
        {
            _inGameEventsObserver = new InGameEventsObserver();
            var testEventDataModel = new TestEventbleModel(_inGameEventsObserver);

            var testEventbleController =
                new TestEventbleController(_inGameEventsObserver, testEventDataModel, _view);
        }
    }
}