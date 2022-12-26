using System.Collections.Generic;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.Components
{
[CreateAssetMenu(fileName = "InteractableObjectsData", menuName = "ScriptableObjects/InteractableObjectsData",
    order = 1)]
public class InteractableObjectsData: ScriptableObject
{
    [field: SerializeField] public List<ScriptableObject> InteractableObjects { get; private set; }
}
}