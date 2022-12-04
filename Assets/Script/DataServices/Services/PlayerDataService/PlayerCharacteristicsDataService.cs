using Script.DataServices.Base;
using Script.DataServices.DataLoader;

namespace Script.DataServices.Services.PlayerDataService
{
public class PlayerCharacteristicsDataService : IDataService
{
    private PlayerDataLoader _playerDataLoader;

    public IDataModel DataModel => _playerDataModel;

    private PlayerDataModel _playerDataModel;

    public PlayerCharacteristicsDataService(IDataLoader playerDataLoader)
    {
        _playerDataLoader = playerDataLoader as PlayerDataLoader;
        _playerDataModel = new PlayerDataModel();

    }

    private void OnCharismaChanged(int pointsDifference)
    {
        _playerDataModel.Charisma += pointsDifference;
    }
}
}