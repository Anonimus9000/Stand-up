using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.UI.Dialogs.BaseDialogs
{
public abstract class UIWindowViewBase : MonoBehaviour, IUIWindow, IView
{
    public abstract IViewModel ViewModel { get; }
    
    protected IUIManager uiManager;

    public abstract void Initialize();

    public void Initialize(IViewModel viewModel)
    {
        
    }

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
}
}