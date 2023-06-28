using System.Collections.Generic;
using UnityEngine;

namespace Script.ProjectLibraries.ConfigParser.FakeConfigData.CharacterCreationData
{
[CreateAssetMenu(fileName = "CharacterCreationData", menuName = "ScriptableObjects/CharacterCreationScriptableObject",
    order = 1)]
public class CharacterCreationData : ScriptableObject, IFakeConfigData
{
    [field: SerializeField] public List<GameObject> CharacterList { get; private set; }

}
}