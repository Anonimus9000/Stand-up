using System;
using Script.ProjectLibraries.UISystem.UIWindow;
using Script.UI.CommonUIs.BaseBehaviour;
using Script.UI.CommonUIs.FullscreenDialogs.CharacterInfo.Characteristics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.CommonUIs.FullscreenDialogs.CharacterInfo
{
public class CharacterInfoView : UiViewBehaviour, IFullScreen
{
    [SerializeField]
    private Button _closeButton;

    [SerializeField]
    private RawImage _avatar;

    [SerializeField]
    private TMP_Text _name;

    [SerializeField]
    private CharacteristicsListView _characteristicsListView;

    public CharacteristicsListView CharacteristicsListView => _characteristicsListView;
    public TMP_Text Name => _name;
    public RawImage Avatar => _avatar;
    public event Action CloseButtonPressed;

    public override void OnShown()
    {
        _closeButton.onClick.AddListener(OnCloseButtonPressed);
    }

    public override void OnHidden()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonPressed);
    }

    private void OnCloseButtonPressed()
    {
        CloseButtonPressed?.Invoke();
    }
}
}