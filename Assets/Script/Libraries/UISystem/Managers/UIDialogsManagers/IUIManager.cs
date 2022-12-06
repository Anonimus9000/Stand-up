using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public interface IUIManager
{
    T Show<T>() where T : IUIWindow, new();
    void Close<T>() where T : IUIWindow, new();
    void CloseWindowsExceptMain();
}
}