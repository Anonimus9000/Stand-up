using System.Collections.Generic;
using Script.UI.Dialogs.PopupDialogs.ActionsPopup.Components;
using UnityEngine;

namespace Script.ConfigData.LocationActionsConfig
{
[CreateAssetMenu(fileName = "ActionData", menuName = "ScriptableObjects/ActionData", order = 1)]
public abstract class LocationActionData : ScriptableObject
{
    public abstract List<ActionFieldData> ActionFields { get; }
    
}
}