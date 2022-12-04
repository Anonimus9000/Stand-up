namespace Script.Libraries.ServiceProvider
{
public interface IServiceProvider
{
    T GetService<T>() where T : IService;
}
}