using Script.Libraries.UISystem.MVx;

namespace Script.UIMVxTest
{
public class SampleDialogController : IUIController
{
    public IUIModel UIModel { get; }

    private SampleDialogController(IUIModel model)
    {
        UIModel = model as SampleDialogModel;
    }

    public void DoSomthingAndSetText(string text)
    {
    }
}
}