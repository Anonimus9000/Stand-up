using UnityEngine;
using UnityEngine.Serialization;

namespace Script.ConfigData.LocationActionsConfig
{
[CreateAssetMenu(fileName = "InteractableObjectsData", menuName = "ScriptableObjects/InteractableObjectsData",
    order = 1)]
public class InteractableObjectsConfig: ScriptableObject, ILocationActionConfig
{
    [FormerlySerializedAs("_computerActionData")]
    [SerializeField]
    private LocationActionData _computerLocationActionData;

    [FormerlySerializedAs("_toiletActionData")]
    [SerializeField]
    private LocationActionData _toiletLocationActionData;

    public LocationActionData ComputerLocationActionData => _computerLocationActionData;
    public LocationActionData ToiletLocationActionData => _toiletLocationActionData;
}
}