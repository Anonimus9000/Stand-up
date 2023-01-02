using UnityEngine;

namespace Script.ConfigData.LocationActionsConfig
{
[CreateAssetMenu(fileName = "InteractableObjectsData", menuName = "ScriptableObjects/InteractableObjectsData",
    order = 1)]
public class InteractableObjectsConfig: ScriptableObject, ILocationActionConfig
{
    [SerializeField]
    private LocationActionData _computerLocationActionData;

    [SerializeField]
    private LocationActionData _toiletLocationActionData;

    public LocationActionData ComputerLocationActionData => _computerLocationActionData;
    public LocationActionData ToiletLocationActionData => _toiletLocationActionData;
}
}