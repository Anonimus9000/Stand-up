using System;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public interface IUIService
{
    IUIViewModel CurrentUI { get; }
    event Action ViewShown;
    event Action ViewClosed;
    event Action ViewHidden;
    
    void Show<T>(IUIViewModel viewModel) where T : IUIView;
    void CloseCurrentView();
    void CloseAll();
}
}