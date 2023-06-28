using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.ProjectLibraries.ConfigParser.FakeConfigData.LocationActionsData
{
[CreateAssetMenu(fileName = "PCActionData", menuName = "ScriptableObjects/PCActionData", order = 1)]
public class PCLocationActionData : LocationActionData
{
    [FormerlySerializedAs("ActionFields")]
    [SerializeField] private List<ActionFieldData> _actionFields;

    public override List<ActionFieldData> ActionFields => _actionFields;

}
}