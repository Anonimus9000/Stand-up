using Script.Libraries.UISystem.MVx;
using UnityEngine;

namespace Script.UIMVxTest
{
public class SampleDialogView : MonoBehaviour, IUIView
{
    public IUIModel UIModel { get; }

    private SampleDialogView(IUIModel model)
    {
        UIModel = model;
    }
}
}