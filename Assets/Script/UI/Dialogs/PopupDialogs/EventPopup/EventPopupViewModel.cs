using System;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UiMVVM;
using Script.Libraries.UISystem.UIWindow;

namespace Script.UI.Dialogs.PopupDialogs.EventPopup
{
public class EventPopupViewModel : IUIViewModel
{
    private EventPopupModel _model;
    public event Action<IUIViewModel> ViewShown;
    public event Action<IUIViewModel> ViewHidden;
    public void ShowView(IUIView view)
    {
        _model = new EventPopupModel();
        _model.BodyText.Subscribe(OnBodyTextChanged);
    }

    private void OnBodyTextChanged(string obj)
    {
        
    }

    public void ShowHiddenView()
    {
        _model.BodyText.Notify("asjodhnfojsdhbviusdbvc");
    }

    public void HideView()
    {
        throw new NotImplementedException();
    }

    public IInstantiatable GetInstantiatable()
    {
        throw new NotImplementedException();
    }
}
}