using System;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.ProjectLibraries.UISystem.UiMVVM
{
public interface IUIViewModel: IViewModel
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