using System;
using Script.Libraries.UISystem.UIWindow;

namespace Script.UI.Dialogs.PopupDialogs.EventPopup
{
public class EventPopupView : IUIView
{
    public event Action ViewShown;
    public event Action ViewHidden;
    public void Show()
    {
        throw new NotImplementedException();
    }

    public void Hide()
    {
        throw new NotImplementedException();
    }
}
}