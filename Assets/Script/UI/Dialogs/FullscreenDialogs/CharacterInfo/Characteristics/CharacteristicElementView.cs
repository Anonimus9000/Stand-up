using Script.ConfigData.LocationActionsConfig;
using Script.Libraries.MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.FullscreenDialogs.CharacterInfo.Characteristics
{
public class CharacteristicElementView : MonoBehaviour, IView
{
    [SerializeField]
    private TMP_Text _characteristicName;

    [SerializeField]
    private TMP_Text _level;
    
    [SerializeField]
    private Slider _progress;

    [SerializeField]
    private RawImage _icon;

    public TMP_Text CharacteristicName => _characteristicName;
    public TMP_Text Level => _level;
    public Slider Progress => _progress;
    public RawImage Icon => _icon;
}
}