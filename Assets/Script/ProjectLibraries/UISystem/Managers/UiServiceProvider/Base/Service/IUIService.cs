using System;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service
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