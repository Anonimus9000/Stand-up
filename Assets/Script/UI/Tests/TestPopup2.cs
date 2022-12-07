using Script.Libraries.MVVM;
using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Script.UI.Tests
{
public class TestPopup2 : UIViewPopupWindow
{
    [SerializeField] private Button _closePopup;
    [SerializeField] private Button _openMainUI;

    public override IModel Model { get; protected set; }

    void Start()
    {
        _closePopup.onClick.AddListener(ClosePopup);
        _openMainUI.onClick.AddListener(OpenMainUI);
    }

    private void OpenMainUI()
    {
        uiManager.Show<TestMainUI2>();
    }

    private void ClosePopup()
    {
        uiManager.Close<TestPopup2>();
    }
}
}