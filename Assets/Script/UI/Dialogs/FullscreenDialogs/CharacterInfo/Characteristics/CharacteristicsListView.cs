using Script.Libraries.MVVM;
using UnityEngine;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterInfo.Characteristics
{
public class CharacteristicsListView : MonoBehaviour, IView
{
    [SerializeField]
    private Transform _characteristicsParent;

    [SerializeField]
    private CharacteristicElementView _characteristicElementPrefab;
    
    public Transform CharacteristicsParent => _characteristicsParent;
    public CharacteristicElementView CharacteristicElementPrefab => _characteristicElementPrefab;
    
}
}