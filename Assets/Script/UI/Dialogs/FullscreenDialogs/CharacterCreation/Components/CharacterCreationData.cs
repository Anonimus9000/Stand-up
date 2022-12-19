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
    //[Serializable]
    // public struct CharacterKeyValuePair
    // {
    //     [field:SerializeField] public int key { get; private set; }
    //     [field:SerializeField] public Sprite data { get; private set; }
    // }

    [field: SerializeField] public List<Sprite> CharacterList { get; private set; }

}
}