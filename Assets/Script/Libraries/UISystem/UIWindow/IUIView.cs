using System;
using Script.Libraries.UISystem.Managers.Instantiater;

namespace Script.Libraries.UISystem.UIWindow
{
public interface IUIView : IInstantiatable
{
    event Action ViewShown;
    event Action ViewHidden;
    
    void Show();
    void Hide();
}
}