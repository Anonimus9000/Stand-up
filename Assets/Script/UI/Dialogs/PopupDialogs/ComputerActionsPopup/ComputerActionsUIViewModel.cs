using Script.Libraries.UISystem.Managers.Instantiater;
using Script.UI.System;

namespace Script.UI.Dialogs.PopupDialogs.ComputerActionsPopup
{
public class ComputerActionsUIViewModel : UiViewModelBehaviour
{
    private ComputerActionsUIView _view;
    private readonly ComputerActionsModel _model;


    public ComputerActionsUIViewModel()
    {
        _model = new ComputerActionsModel();
    }

    public override void ShowView()
    {
        _view = popupsUiManager.Show<ComputerActionsUIView>(this);
        SubscribeOnViewEvent(_view);
    }

    public override void CloseView()
    {
        popupsUiManager.TryCloseCurrent();
    }

    public override IInstantiatable GetInstantiatable()
    {
        return _view;
    }

    private void SubscribeOnViewEvent(ComputerActionsUIView view)
    {
        view.OnClosePressed += OnCloseButtonPressed;
    }

    private void OnCloseButtonPressed()
    {
        popupsUiManager.TryCloseCurrent();
    }
}
}