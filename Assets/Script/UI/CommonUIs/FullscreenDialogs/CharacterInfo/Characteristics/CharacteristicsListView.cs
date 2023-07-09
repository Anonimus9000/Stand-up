using Script.ProjectLibraries.MVVM;
using UnityEngine;

namespace Script.UI.CommonUIs.FullscreenDialogs.CharacterInfo.Characteristics
{
public class CharacteristicsListView : ViewBehaviour
{
    [SerializeField]
    private Transform _characteristicsParent;

    [SerializeField]
    private CharacteristicElementView _characteristicElementPrefab;
    
    public Transform CharacteristicsParent => _characteristicsParent;
    public CharacteristicElementView CharacteristicElementPrefab => _characteristicElementPrefab;
}
}