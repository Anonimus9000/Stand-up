﻿using System;
using Script.Libraries.MVVM;
using Script.UI.Dialogs.BaseDialogs;
using Script.UI.Dialogs.FullscreenDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.MainUI
{
public class MainHomeUI : UIViewMain
{
    [SerializeField] private Button _openFullscreenButton;
    [SerializeField] private Button _openCharacterInfoButton;

    public override IModel Model { get; protected set; }

    private void Start()
    {
        _openFullscreenButton.onClick.AddListener(OpenStartFullscreen);
        _openCharacterInfoButton.onClick.AddListener(OpenCharacterInfo);
    }

    private void OpenCharacterInfo()
    {
        uiManager.Show<CharacterInfoFullScreen>();
    }

    private void OpenStartFullscreen()
    {
        uiManager.Show<StartFullScreen>();
    }
}
}