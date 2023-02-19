using Script.ConfigData.PlayerDataConfig;

namespace Script.DataServices.DataLoader
{
public class PlayerDataLoader : IDataLoader
{
    public  PlayerData PlayerData { get; }

    public PlayerDataLoader(PlayerData playerData)
    {
        PlayerData = playerData;
    }
    
    public void LoadData()
    {
        
    }
}
}