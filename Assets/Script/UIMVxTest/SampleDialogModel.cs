using System;
using Script.Libraries.UISystem.MVx;

namespace Script.UIMVxTest
{
public class SampleDialogModel : IUIModel
{
    private string _text;

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            TextChanged?.Invoke();
        }
    }

    public event Action TextChanged;
}
}