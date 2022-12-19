using System;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.UiMVVM
{
public interface IUIViewModel : IViewModel
{
    event Action<IUIViewModel> ViewShown;
    event Action<IUIViewModel> ViewHidden;
    public void ShowView(IUIView view);
    public void ShowHiddenView();
    public void HideView();
    public IInstantiatable GetInstantiatable();
}
}