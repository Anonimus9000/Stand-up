using Script.DataServices;
using Script.DataServices.DataLoader;
using Script.DataServices.Services.PlayerDataService;
using Script.Initializer.Base;
using Script.Libraries.ServiceProvider;

namespace Script.Initializer.StartApplicationDependenciesInitializers
{
public class DataServiceProviderInitializer : IDependenciesInitializer
{
    public IInitializable Initialize()
    {
        var services = GetServices();
        var dataServiceProvider = new DataServiceProvider(services);

        return dataServiceProvider;
    }

    private IService[] GetServices()
    {
        IDataLoader playerDataLoader = InitializePlayerDataLoader();
        
        return new IService[]
        {
            new PlayerCharacteristicsDataService(playerDataLoader)
        };
    }

    private static PlayerDataLoader InitializePlayerDataLoader()
    {
        var playerDataLoader = new PlayerDataLoader();
        playerDataLoader.LoadData();

        return playerDataLoader;
    }
}
}