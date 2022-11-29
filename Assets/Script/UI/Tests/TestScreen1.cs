using Script.Libraries.MVVM;
using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Tests
{
public class TestScreen1 : UIViewFullscreen
{
    [SerializeField] private Button _openSecondScreen;

    public override IViewModel ViewModel { get; }
    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        _openSecondScreen.onClick.AddListener(OpenScreen);
    }

    private void OpenScreen()
    {
        uiManager.Show<TestScreen2>();
    }
}
}