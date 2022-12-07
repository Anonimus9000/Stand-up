using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using Script.SceneSwitcherSystem.Switcher;
using UnityEngine;

namespace Script.UI.Dialogs.BaseDialogs
{
public abstract class UIWindowViewBase : MonoBehaviour, IUIWindow, IView
{
    public abstract IModel Model { get; protected set; }
    
    protected IUIManager uiManager;

    protected ISceneSwitcher sceneSwitcher;

    public virtual void Initialize(IModel model)
    {
        Model = model;
    }

    public virtual void SetUiManager(IUIManager uiManager)
    {
        this.uiManager = uiManager;
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
}
}