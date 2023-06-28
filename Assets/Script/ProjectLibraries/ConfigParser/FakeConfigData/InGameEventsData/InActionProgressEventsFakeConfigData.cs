using UnityEngine;

namespace Script.ProjectLibraries.ConfigParser.FakeConfigData.InGameEventsData
{
[CreateAssetMenu(fileName = "InGameActionProgressConfig", menuName = "ScriptableObjects/InGameActionProgressConfig",
    order = 2)]
public class InActionProgressEventsFakeConfigData : ScriptableObject, IFakeConfigData
{
    [Range(0, 100)]
    [SerializeField]
    private int _changeToShowEvent;

    [SerializeField]
    private int _minProgressPercentToShowEvent;

    [SerializeField]
    private int _maxProgressPercentToShowEvent;

    [SerializeField]
    private float _howOftenTryShowEventPerSecond;

    public int ChangeToShowEvent => _changeToShowEvent;
    public int MinProgressPercentToShowEvent => _minProgressPercentToShowEvent;
    public int MaxProgressPercentToShowEvent => _maxProgressPercentToShowEvent;
    public float HowOftenTryShowEventPerSecond => _howOftenTryShowEventPerSecond;
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (_maxProgressPercentToShowEvent == 0)
        {
            _maxProgressPercentToShowEvent = 100;
        }

        if (_minProgressPercentToShowEvent > _maxProgressPercentToShowEvent)
        {
            (_minProgressPercentToShowEvent, _maxProgressPercentToShowEvent) =
                (_maxProgressPercentToShowEvent, _minProgressPercentToShowEvent);
        }

        if (_minProgressPercentToShowEvent == _maxProgressPercentToShowEvent)
        {
            _minProgressPercentToShowEvent--;
            _maxProgressPercentToShowEvent++;
        }
    }
    #endif
}
}