using System;
using Script.Libraries.MVVM;
using Script.Libraries.Observer.ObservableValue;

namespace Script.UI.Dialogs.PopupDialogs.EventPopup
{
public class EventPopupModel : IModel
{
    public ObservableValue<string> BodyText;
    
}
}