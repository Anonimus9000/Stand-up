using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UiMVVM;
using Script.UI.System;

namespace Script.UI.Dialogs.PopupDialogs.ComputerActionsPopup
{
public class ComputerActionsViewModel : UiViewModelBehaviour
{
    private ComputerActionsView _view;
    private readonly ComputerActionsModel _model;

    public ComputerActionsViewModel()
    {
        _model = new ComputerActionsModel();
    }

    public override void ShowView()
    {
        _view = popupsUiManager.Show<ComputerActionsView>(this);
    }

    public override void CloseView()
    {
        popupsUiManager.TryCloseCurrent();
    }

    public override IInstantiatable GetInstantiatable()
    {
        return _view;
    }
}
}