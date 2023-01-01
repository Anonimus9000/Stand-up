using Script.Libraries.UISystem.Managers.UiServiceProvider.Base.Service;

namespace Script.Libraries.UISystem.Managers.UiServiceProvider.Base.ServiceProvider
{
public interface IUIServiceProvider
{
    public T GetService<T>() where T : IUIService;
}
}