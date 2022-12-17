using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public interface IDialogsManager
{
    public UIViewModel Current { get; }
    T Show<T>(UIViewModel viewModel) where T : IUIView;
    bool TryCloseCurrent();
    void CloseAll();
}
}