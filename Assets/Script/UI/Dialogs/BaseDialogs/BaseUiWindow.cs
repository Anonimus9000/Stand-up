using Script.Libraries.UISystem.Managers.UIDialogsManagers;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.UI.Dialogs.BaseDialogs
{
public abstract class BaseUiWindow : MonoBehaviour, IUIWindow
{
    protected IUIManager uiManager;

    public void Initialize(IUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public void OnShown()
    {
    }

    public void OnHidden()
    {
    }
}
}