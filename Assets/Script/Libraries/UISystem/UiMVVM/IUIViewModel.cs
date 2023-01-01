using System;
using Script.Libraries.MVVM;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.UiMVVM
{
public interface IUIViewModel : IViewModel
{
    event Action<IUIViewModel> ViewShown;
    event Action<IUIViewModel> ViewHidden;
    public void Init(IUIView view, IAnimatorService animatorService);
    public void Deinit();
    public void ShowView();
    public void ShowHiddenView();
    public void HideView();
    public IInstantiatable GetInstantiatable();
}
}