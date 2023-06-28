namespace Script.ProjectLibraries.ServiceLocators
{
public interface IDataServiceLocator
{
    T GetService<T>() where T : IService;
}
}
