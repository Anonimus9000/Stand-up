using System;
using Script.Libraries.MVVM;
using Script.SceneSwitcherSystem.Container.Scenes.Home;
using Script.UI.Dialogs.BaseDialogs;
using Script.UI.Dialogs.MainUI;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.FullscreenDialogs.ApplicationEnter
{
public class ApplicationEnterView : UIViewFullscreen
{
    [SerializeField] private Button _startButton;

    [SerializeField] private Button _closeButton;

   public event Action OnQuitPressed;

    public override IModel Model { get; protected set; }
    public event Action OnStartPressed;

    public override void OnShown()
    {
        base.OnShown();

        _startButton.onClick.AddListener(StartButton);
        _closeButton.onClick.AddListener(CloseButton);


        var model = new ApplicationEnterModel();
        var applicationEnterViewModel = new ApplicationEnterViewModel(this, model, uiManager, sceneSwitcher);
    }

    public override void OnHidden()
    {
        base.OnHidden();

        _startButton.onClick.RemoveListener(StartButton);
        _closeButton.onClick.RemoveListener(CloseButton);
    }

    private void CloseButton()
    {
        OnQuitPressed?.Invoke();
    }

    private void StartButton()
    {
        OnStartPressed?.Invoke();
    }
}
}

