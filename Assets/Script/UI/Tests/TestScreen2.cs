using Script.Libraries.MVVM;
using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Tests
{
public class TestScreen2 : UIViewFullscreen
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
        uiManager.Close<TestScreen2>();
    }

    private void OpenPopup()
    {
        uiManager.Show<TestPopup1>();
    }

    protected override void InitializeViewModel(IViewModel viewModel)
    {
        
    }
}
}