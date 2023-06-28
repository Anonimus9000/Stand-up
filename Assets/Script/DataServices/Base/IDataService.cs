using Script.ProjectLibraries.ServiceLocators;

namespace Script.DataServices.Base
{
public interface IDataService : IService
{
    IDataContainer DataContainer { get; }
}
}