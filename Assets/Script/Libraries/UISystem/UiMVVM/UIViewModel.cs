using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;

namespace Script.Libraries.UISystem.UiMVVM
{
public abstract class UIViewModel : IModel
{
    protected IDialogsManager mainUiManager;
    protected IDialogsManager popupsUiManager;
    protected IDialogsManager fullScreensUiManager;
        
    public void InitializeUiManagers(
        IDialogsManager mainUiManager, 
        IDialogsManager popupsUiManager,
        IDialogsManager fullScreensUiManager)
    {
        this.mainUiManager = mainUiManager;
        this.popupsUiManager = popupsUiManager;
        this.fullScreensUiManager = fullScreensUiManager;
    }

    public abstract void ShowView();
    public abstract void CloseView();
    public abstract IInstantiatable GetInstantiatable();
}
}