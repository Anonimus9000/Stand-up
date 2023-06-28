using UnityEngine;

namespace Script.ProjectLibraries.ConfigParser.FakeConfigData.LocationActionsData
{
[CreateAssetMenu(fileName = "InteractableObjectsData", menuName = "ScriptableObjects/InteractableObjectsData",
    order = 1)]
public class InteractableObjectsFakeConfigData: ScriptableObject, IFakeConfigData
{
    [SerializeField]
    private LocationActionData _computerLocationActionData;

    [SerializeField]
    private LocationActionData _toiletLocationActionData;

    public LocationActionData ComputerLocationActionData => _computerLocationActionData;
    public LocationActionData ToiletLocationActionData => _toiletLocationActionData;
}
}