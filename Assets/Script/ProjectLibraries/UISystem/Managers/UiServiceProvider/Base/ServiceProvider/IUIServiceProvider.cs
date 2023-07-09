using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;
using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider
{
public interface IUIServiceProvider
{
    T GetService<T>() where T : IUIService;
    void Show<T>(IUIViewModel viewModel) where T : IUIView;
    void CloseCurrentView(UIType uiType);
    void CloseAll(UIType uiType);
    IUIViewModel GetCurrentUI(UIType uiType);
}
}