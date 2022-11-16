using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Libraries.UISystem.Tests
{
    public class TestPopup2: BasePopupDialog
    {
        [SerializeField] private Button _closePopup;

        void Start()
        {
            _closePopup.onClick.AddListener(ClosePopup);
        }

        private void ClosePopup()
        {
            UIManager.Close<TestPopup2>();
        }
    }
}