using System.Collections.Generic;
using UnityEngine;

namespace Script.ProjectLibraries.ConfigParser.FakeConfigData.LocationActionsData
{
[CreateAssetMenu(fileName = "ActionData", menuName = "ScriptableObjects/ActionData", order = 1)]
public abstract class LocationActionData : ScriptableObject
{
    public abstract List<ActionFieldData> ActionFields { get; }
    
}
}