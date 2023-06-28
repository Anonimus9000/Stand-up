using Script.ProjectLibraries.ConfigParser.Base;
using Script.ProjectLibraries.ConfigParser.FakeConfigData.LocationActionsData;

namespace Script.ProjectLibraries.ConfigParser.Configs.FakeConfigs
{
public class FakeInteractableObjectsConfig : IInteractableObjectsConfig
{
    public LocationActionData ComputerLocationActionData { get; }
    public LocationActionData ToiletLocationActionData { get; }
    
    public FakeInteractableObjectsConfig(LocationActionData toiletLocationActionData, LocationActionData computerLocationActionData)
    {
        ToiletLocationActionData = toiletLocationActionData;
        ComputerLocationActionData = computerLocationActionData;
    }
}
}