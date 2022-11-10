using Script.Libraries.UISystem.Managers.Instantiater;

namespace Script.Libraries.UISystem.UIWindow
{
    public interface IUIWindow : IInstantiatble
    {
        void OnShown();

        void OnHidden();
    }
}