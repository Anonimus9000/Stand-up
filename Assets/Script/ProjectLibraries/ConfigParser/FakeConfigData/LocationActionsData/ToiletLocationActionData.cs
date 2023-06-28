using System.Collections.Generic;
using UnityEngine;

namespace Script.ProjectLibraries.ConfigParser.FakeConfigData.LocationActionsData
{
[CreateAssetMenu(fileName = "ToiletActionData", menuName = "ScriptableObjects/ToiletActionData", order = 1)]
public class ToiletLocationActionData : LocationActionData
{
    [SerializeField]
    private List<ActionFieldData> _actionFields;

    public override List<ActionFieldData> ActionFields => _actionFields;
}
}