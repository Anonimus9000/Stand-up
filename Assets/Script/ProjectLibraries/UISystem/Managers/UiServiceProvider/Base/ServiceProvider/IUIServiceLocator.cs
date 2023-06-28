using Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.Service;

namespace Script.ProjectLibraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider
{
public interface IUIServiceLocator
{
    public T GetService<T>() where T : IUIService;
}
}