using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.UI.Dialogs.BaseDialogs
{
public abstract class UIWindowViewBase : MonoBehaviour, IUIWindow, IView
{
    protected IUIManager uiManager;

    public virtual void InitializeWindow(IUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public virtual void OnShown()
    {
    }

    public virtual void OnHidden()
    {
    }

    protected abstract void InitializeMVVM();
}
}