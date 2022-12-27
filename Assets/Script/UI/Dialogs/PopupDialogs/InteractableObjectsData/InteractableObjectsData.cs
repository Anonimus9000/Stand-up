using System.Collections.Generic;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.InteractableObjectsData
{
[CreateAssetMenu(fileName = "InteractableObjectsData", menuName = "ScriptableObjects/InteractableObjectsData",
    order = 1)]
public class InteractableObjectsData: ScriptableObject
{
    [field: SerializeField] public List<ActionData> InteractableObjects { get; private set; }
}
}