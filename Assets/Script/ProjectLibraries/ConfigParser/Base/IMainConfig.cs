namespace Script.ProjectLibraries.ConfigParser.Base
{
public interface IMainConfig : IConfig
{
    public ICharacterModelsConfig CharacterModelsConfig { get; }
    public IInActionProgressConfig InActionProgressConfig { get; }
    public IPlayerDataConfig PlayerDataConfig { get; }
    public IInteractableObjectsConfig FakeInteractableObjectsConfig { get; }
}
}