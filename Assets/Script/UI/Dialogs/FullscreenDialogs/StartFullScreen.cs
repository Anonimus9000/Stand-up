using Script.Libraries.MVVM;
using Script.UI.Dialogs.BaseDialogs;
using Script.UI.Dialogs.MainUI;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.FullscreenDialogs
{
public class StartFullScreen: UIViewFullscreen
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _closeButton;

    private void Start()
    {
        _startButton.onClick.AddListener(StartButton);
        _closeButton.onClick.AddListener(CloseButton);
    }

    private void CloseButton()
    {
        Application.Quit();
    }

    private void StartButton()
    {
        uiManager.Show<MainHomeUI>();
    }
    

    public override IViewModel ViewModel { get; }
    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
}