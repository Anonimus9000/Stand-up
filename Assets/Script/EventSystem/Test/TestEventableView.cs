using TMPro;
using UnityEngine;

namespace Script.EventSystem.Test
{
public class TestEventableView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    public void CreatGO()
    {
        Instantiate(new GameObject());
    }

    public void SetText(string text)
    {
        _textMeshProUGUI.text = text;
    }
}
}