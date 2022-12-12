using Script.Libraries.MVVM;
using Script.UI.Dialogs.BaseDialogs;
using Script.UI.Dialogs.FullscreenDialogs.CharacterInfo;
using Script.UI.Dialogs.FullscreenDialogs.StartGameMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.MainUI.MainHome
{
public class HomeUIView : MonoUIMain
{
    [SerializeField] private Button _openFullscreenButton;
    [SerializeField] private Button _openCharacterInfoButton;

    private void Start()
    {
        _openFullscreenButton.onClick.AddListener(OpenStartFullscreen);
        _openCharacterInfoButton.onClick.AddListener(OpenCharacterInfo);
    }

    private void OpenCharacterInfo()
    {
        uiSystem.Show(new CharacterInfoViewModel());
    }

    private void OpenStartFullscreen()
    {
        uiSystem.Show(new StartGameMenuViewModel(uiSystem, sceneSwitcher));
    }
}
}