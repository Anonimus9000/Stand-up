﻿using Script.Libraries.MVVM;
using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Tests
{
public class TestPopup1 : UIViewPopupWindow
{
    [SerializeField] private Button _openPopup;
    [SerializeField] private Button _closePopup;
    
    public override IViewModel ViewModel { get; }
    public override void Initialize()
    {
    }

    private void Start()
    {
        _openPopup.onClick.AddListener(OpenPopup);
        _closePopup.onClick.AddListener(ClosePopup);
    }

    private void ClosePopup()
    {
        uiManager.Close<TestPopup1>();
    }

    private void OpenPopup()
    {
        uiManager.Show<TestPopup2>();
    }
}
}