using Script.ConfigData.PlayerDataConfig;
using Script.DataServices;
using Script.DataServices.DataLoader;
using Script.DataServices.Services.PlayerDataService;
using Script.Initializer.Base;
using Script.Libraries.ServiceProvider;

namespace Script.Initializer.StartApplicationDependenciesInitializers
{
public class DataServiceProviderInitializer : IDependenciesInitializer
{
    private readonly PlayerData _playerData;

    public DataServiceProviderInitializer(PlayerData playerData)
    {
        _playerData = playerData;
    }
    
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
            new PlayerDataService(playerDataLoader)
        };
    }

    private PlayerDataLoader InitializePlayerDataLoader()
    {
        var playerDataLoader = new PlayerDataLoader(_playerData);
        playerDataLoader.LoadData();

        return playerDataLoader;
    }
}
}