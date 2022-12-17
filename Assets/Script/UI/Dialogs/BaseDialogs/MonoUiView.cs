using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.UI.Dialogs.BaseDialogs
{
public abstract class MonoUiView : MonoBehaviour, IUIView
{
    protected IUISystem uiSystem;

    public virtual void SetUiManager(IUISystem uiSystem)
    {
        this.uiSystem = uiSystem;
    }

    public virtual void OnShown()
    {
    }

    public virtual void OnHidden()
    {
    }

    public virtual void OnClose()
    {
    }
}
}