using System.Collections.Generic;
using Script.ProjectLibraries.ConfigParser.Base;
using UnityEngine;

namespace Script.ProjectLibraries.ConfigParser.Configs.FakeConfigs
{
public class FakeCharacterModelsConfig : ICharacterModelsConfig
{
    public List<GameObject> CharacterModels { get; }

    public FakeCharacterModelsConfig(List<GameObject> characterModels)
    {
        CharacterModels = characterModels;
    }
}
}