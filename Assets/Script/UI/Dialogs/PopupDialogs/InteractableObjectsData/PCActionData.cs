using System.Collections.Generic;
using Script.UI.Dialogs.PopupDialogs.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.UI.Dialogs.PopupDialogs.InteractableObjectsData
{
[CreateAssetMenu(fileName = "PCActionData", menuName = "ScriptableObjects/PCActionData",
    order = 1)]
public class PCActionData : ActionData
{
    [FormerlySerializedAs("ActionFields")]
    [SerializeField] private List<ActionFieldContent> _actionFields;

    public override List<ActionFieldContent> ActionFields => _actionFields;

}
}