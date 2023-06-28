using Script.ProjectLibraries.ConfigParser.FakeConfigData.LocationActionsData;

namespace Script.ProjectLibraries.ConfigParser.Base
{
public interface IInteractableObjectsConfig
{
    public LocationActionData ComputerLocationActionData { get; }
    public LocationActionData ToiletLocationActionData { get; }
}
}