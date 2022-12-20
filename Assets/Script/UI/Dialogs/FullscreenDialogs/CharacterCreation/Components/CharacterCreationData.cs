using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterCreation.Components
{
[CreateAssetMenu(fileName = "CharacterCreationData", menuName = "ScriptableObjects/CharacterCreationScriptableObject",
    order = 1)]
public class CharacterCreationData : ScriptableObject
{
    [field: SerializeField] public List<GameObject> CharacterList { get; private set; }

}
}