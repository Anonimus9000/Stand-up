using System;
using Script.Libraries.MVVM;
using Script.UI.Dialogs.BaseDialogs;
using Script.UI.Dialogs.FullscreenDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.MainUI
{
public class MainHomeUI:UIViewMain
{
    [SerializeField] private Button _openFullscreenButton;

    private void Start()
    {
        _openFullscreenButton.onClick.AddListener(OpenStartFullscreen);
    }

    private void OpenStartFullscreen()
    {
        uiManager.Show<StartFullScreen>();
    }

    protected override void InitializeViewModel(IViewModel viewModel)
    {
        
    }
}
}