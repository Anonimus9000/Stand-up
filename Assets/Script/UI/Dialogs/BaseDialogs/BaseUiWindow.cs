using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.UI.Dialogs.BaseDialogs
{
    public abstract class BaseUiWindow : MonoBehaviour, IUIWindow
    {
        public void OnShown() { }

        public void OnHidden() { }
    }
}