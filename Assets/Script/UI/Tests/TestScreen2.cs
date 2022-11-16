using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Tests
{
public class TestScreen2 : BaseFullscreenWindow
{
    [SerializeField] private Button _openPopup;
    [SerializeField] private Button _closeButton;

    private void Start()
    {
        _openPopup.onClick.AddListener(OpenPopup);
        _closeButton.onClick.AddListener(CloseScreen);
    }

    private void CloseScreen()
    {
        UIManager.Close<TestScreen2>();
    }

    private void OpenPopup()
    {
        UIManager.Show<TestPopup1>();
    }
}
}