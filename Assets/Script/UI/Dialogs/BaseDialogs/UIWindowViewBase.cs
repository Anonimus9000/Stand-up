using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.UI.Dialogs.BaseDialogs
{
public abstract class UIWindowViewBase : MonoBehaviour, IUIWindow, IView
{
    public abstract IModel Model { get; protected set; }
    
    protected IUIManager uiManager;

    public virtual void Initialize(IModel model)
    {
        Model = model;
    }

    public virtual void InitializeDependencies(IUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public virtual void OnShown()
    {
    }

    public virtual void OnHidden()
    {
    }
}
}