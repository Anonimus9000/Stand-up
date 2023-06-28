using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.ConfigParser.Configs.FakeConfigs;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.CharacterCreationData;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.InGameEventsData;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.LocationActionsData;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.PlayerDataData;

namespace Script.ProjectLibraries.ConfigParser.Parsers.Fake
{
public class FakeMainConfigParser : IMainConfigParser
{
    private readonly PlayerDataFakeConfigData _playerDataFakeConfigData;
    private readonly CharacterCreationData _characterCreationData;
    private readonly InActionProgressEventsFakeConfigData _inActionProgressEventsFakeConfigData;
    private readonly InteractableObjectsFakeConfigData _interactableObjectsFakeConfigData;

    public FakeMainConfigParser(
        InActionProgressEventsFakeConfigData inActionProgressEventsFakeConfigData,
        PlayerDataFakeConfigData playerDataFakeConfigData,
        CharacterCreationData characterCreationData, 
        InteractableObjectsFakeConfigData interactableObjectsFakeConfigData)
    {
        _inActionProgressEventsFakeConfigData = inActionProgressEventsFakeConfigData;
        _playerDataFakeConfigData = playerDataFakeConfigData;
        _characterCreationData = characterCreationData;
        _interactableObjectsFakeConfigData = interactableObjectsFakeConfigData;
    }
    
    public IMainConfig ParseMainConfig()
    {
        var fakeCharacterModelsConfig = new FakeCharacterModelsConfig(_characterCreationData.CharacterList);
        var fakePlayerDataConfig = new FakePlayerDataConfig(_playerDataFakeConfigData.Name,
            _playerDataFakeConfigData.CharacteristicsData, _playerDataFakeConfigData.Avatar);
        var fakeInActionProgressConfig = new FakeInActionProgressConfig(
            _inActionProgressEventsFakeConfigData.ChangeToShowEvent,
            _inActionProgressEventsFakeConfigData.MinProgressPercentToShowEvent,
            _inActionProgressEventsFakeConfigData.MaxProgressPercentToShowEvent,
            _inActionProgressEventsFakeConfigData.HowOftenTryShowEventPerSecond);
        var fakeInteractableObjectsConfig = new FakeInteractableObjectsConfig(_interactableObjectsFakeConfigData.ToiletLocationActionData,
            _interactableObjectsFakeConfigData.ComputerLocationActionData);

        return new FakeMainConfig(fakeCharacterModelsConfig, fakeInActionProgressConfig, fakePlayerDataConfig, fakeInteractableObjectsConfig);
    }
}
}