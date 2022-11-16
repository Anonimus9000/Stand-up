using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public interface IUIManager
{
    IUIWindow Show<T>() where T : IUIWindow, new();
    void Close<T>() where T : IUIWindow, new();
}
}