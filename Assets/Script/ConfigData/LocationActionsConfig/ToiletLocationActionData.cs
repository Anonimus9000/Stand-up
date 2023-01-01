using System.Collections.Generic;
using Script.UI.Dialogs.PopupDialogs.ActionsPopup.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.ConfigData.LocationActionsConfig
{
[CreateAssetMenu(fileName = "ToiletActionData", menuName = "ScriptableObjects/ToiletActionData", order = 1)]
public class ToiletLocationActionData: LocationActionData
{

    [FormerlySerializedAs("ActionFields")] 
    [SerializeField] private List<ActionFieldData> _actionFields;
    
    public override List<ActionFieldData> ActionFields => _actionFields;

}
}