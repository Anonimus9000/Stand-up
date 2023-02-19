using Script.Libraries.ServiceProvider;

namespace Script.DataServices.Base
{
public interface IDataService : IService
{
    IDataContainer DataContainer { get; }
}
}