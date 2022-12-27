using System.Collections.Generic;
using Script.UI.Dialogs.PopupDialogs.Components;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.InteractableObjectsData
{
public abstract class ActionData : ScriptableObject
{
    public abstract List<ActionField> ActionFields { get; }
    
}
}