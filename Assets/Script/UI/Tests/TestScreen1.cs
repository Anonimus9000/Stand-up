using Script.Libraries.MVVM;
using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Tests
{
public class TestScreen1 : UIViewFullscreen
{
    [SerializeField] private Button _openSecondScreen;

    private void Start()
    {
        _openSecondScreen.onClick.AddListener(OpenScreen);
    }

    private void OpenScreen()
    {
        uiManager.Show<TestScreen2>();
    }

    protected override void InitializeViewModel(IViewModel viewModel)
    {
        
    }
}
}