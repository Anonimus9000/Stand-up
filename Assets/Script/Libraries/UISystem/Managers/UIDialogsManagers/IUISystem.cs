using Script.Libraries.UISystem.UiMVVM;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public interface IUISystem
{
    UIViewModel Show(UIViewModel viewModel);
    void Close(UIViewModel viewModel);
    void CloseWindowsExceptMain();
}
}