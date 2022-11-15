using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.UI.Dialogs.BaseDialogs
{
    public abstract class BaseUiWindow : MonoBehaviour, IUIWindow
    {
        protected UIManager UIManager;

        public void Initialize(UIManager uiManager)
        {
            UIManager = uiManager;
        }

        public void OnShown() { }

        public void OnHidden() { }
    }
}