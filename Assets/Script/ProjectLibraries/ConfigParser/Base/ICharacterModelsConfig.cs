using System.Collections.Generic;
using UnityEngine;

namespace Script.ProjectLibraries.ConfigParser.Base
{
public interface ICharacterModelsConfig : IConfig
{
    public List<GameObject> CharacterModels { get; }
}
}