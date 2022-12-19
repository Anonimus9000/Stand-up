namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
public interface IUIServiceProvider
{
    public T GetService<T>() where T : IUIService;
}
}