using Script.ProjectLibraries.ConfigParser.Base;

namespace Script.ProjectLibraries.ConfigParser.Configs.FakeConfigs
{
public class FakeMainConfig : IMainConfig
{
    public ICharacterModelsConfig CharacterModelsConfig { get; }
    public IInActionProgressConfig InActionProgressConfig { get; }
    public IPlayerDataConfig PlayerDataConfig { get; }
    public IInteractableObjectsConfig FakeInteractableObjectsConfig { get; }

    public FakeMainConfig(
        FakeCharacterModelsConfig fakeCharacterModelsConfig, 
        FakeInActionProgressConfig fakeInActionProgressConfig, 
        FakePlayerDataConfig fakePlayerDataConfig, 
        FakeInteractableObjectsConfig fakeInteractableObjectsConfig)
     {
         CharacterModelsConfig = fakeCharacterModelsConfig;
         InActionProgressConfig = fakeInActionProgressConfig;
         PlayerDataConfig = fakePlayerDataConfig;
         FakeInteractableObjectsConfig = fakeInteractableObjectsConfig;
     }
}
}