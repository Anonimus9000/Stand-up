using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Switcher;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Script.UI.Dialogs.BaseDialogs
{
public abstract class MonoUiView : MonoBehaviour, IUIView
{
    protected IUISystem uiSystem;

    protected ISceneSwitcher sceneSwitcher;

    public virtual void SetUiManager(IUISystem uiSystem)
    {
        this.uiSystem = uiSystem;
    }

    public void InitializeDependencies(ISceneSwitcher sceneSwitcher)
    {
        this.sceneSwitcher = sceneSwitcher;
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