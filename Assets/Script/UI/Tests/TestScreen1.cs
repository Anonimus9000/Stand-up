using Script.UI.Dialogs.BaseDialogs;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Tests
{
public class TestScreen1 : BaseFullscreenWindow
{
    [SerializeField] private Button _openSecondScreen;

    private void Start()
    {
        _openSecondScreen.onClick.AddListener(OpenScreen);
    }

    private void OpenScreen()
    {
        UIManager.Show<TestScreen2>();
    }
}
}