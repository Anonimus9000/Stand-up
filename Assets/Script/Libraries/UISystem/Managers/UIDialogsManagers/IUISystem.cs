using Script.Libraries.UISystem.UiMVVM;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public interface IUISystem
{
    public UIViewModel CurrentMain { get; }
    public UIViewModel CurrentFullScreen { get; }
    public UIViewModel CurrentPopup { get; }
    
    UIViewModel Show(UIViewModel viewModel);
    void Close(UIViewModel viewModel);
    void CloseWindowsExceptMain();
    void CloseAllPopups();
}
}