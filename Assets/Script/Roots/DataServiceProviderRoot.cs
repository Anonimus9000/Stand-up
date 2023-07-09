using Script.DataServices;
using Script.DataServices.DataLoader;
using Script.DataServices.Services.PlayerDataService;
using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.Root;
using Script.ProjectLibraries.ServiceLocators;

namespace Script.Roots
{
public class DataServiceProviderRoot : IRoot
{
    private readonly IPlayerDataConfig _playerDataFakeConfig;

    public DataServiceProviderRoot(IPlayerDataConfig playerDataFakeConfig)
    {
        _playerDataFakeConfig = playerDataFakeConfig;
    }
    
    public DataServiceDataServiceLocator Initialize()
    {
        var services = GetServices();
        var dataServiceProvider = new DataServiceDataServiceLocator(services);

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
        var playerDataLoader = new PlayerDataLoader(_playerDataFakeConfig);
        playerDataLoader.LoadData();

        return playerDataLoader;
    }
}
}