using Script.Libraries.UISystem.Managers.Instantiater;
using Script.UI.Dialogs.PopupDialogs.ComputerActionsPopup;
using Script.UI.System;
using NotImplementedException = System.NotImplementedException;

namespace Script.UI.Dialogs.PopupDialogs.ToiletActionsPopup
{
public class ToiletUIActionsViewModel: UiViewModelBehaviour
{
    private ToiletUIActionsView _view;
    private readonly ToiletUIActionsModel _model;
    public ToiletUIActionsViewModel()
    {
        _model = new ToiletUIActionsModel();
    }

    public override void ShowView()
    {
        _view = popupsUiManager.Show<ToiletUIActionsView>(this);
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

    private void SubscribeOnViewEvent(ToiletUIActionsView view)
    {
        view.OnClosePressed += OnCloseButtonPressed;
    }

    private void OnCloseButtonPressed()
    {
        popupsUiManager.TryCloseCurrent();
    }
}
}