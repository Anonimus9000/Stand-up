using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UIDialogsManagers;

namespace Script.Libraries.UISystem.UIWindow
{
    public interface IUIWindow : IInstantiatble
    {
        void Initialize(UIManager uiManager);
        void OnShown();

        void OnHidden();
    }
}