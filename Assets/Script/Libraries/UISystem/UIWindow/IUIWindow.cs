using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;

namespace Script.Libraries.UISystem.UIWindow
{
public interface IUIWindow : IInstantiatable
{
    void SetUiManager(IUIManager uiManager);
    void OnShown();

    void OnHidden();
}
}