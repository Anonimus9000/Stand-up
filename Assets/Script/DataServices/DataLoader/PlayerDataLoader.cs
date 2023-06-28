using Script.ProjectLibraries.ConfigParser.Base;

namespace Script.DataServices.DataLoader
{
public class PlayerDataLoader : IDataLoader
{
    public  IPlayerDataConfig PlayerDataFakeConfig { get; }

    public PlayerDataLoader(IPlayerDataConfig playerDataFakeConfig)
    {
        PlayerDataFakeConfig = playerDataFakeConfig;
    }
    
    public void LoadData()
    {
        
    }
}
}