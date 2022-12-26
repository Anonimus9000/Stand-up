using System.Collections.Generic;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.Components
{
[CreateAssetMenu(fileName = "PCActionData", menuName = "ScriptableObjects/PCActionData",
    order = 1)]
public class PCActionData : ScriptableObject
{
   [field:SerializeField] public List<ActionField> ActionFields { get; private set; }

}
}