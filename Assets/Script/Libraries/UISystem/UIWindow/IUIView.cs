using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;

namespace Script.Libraries.UISystem.UIWindow
{
public interface IUIView : IInstantiatable
{
    void SetUiManager(IUISystem uiSystem);
    void OnShown();
    void OnHidden();
    void OnClose();
}
}