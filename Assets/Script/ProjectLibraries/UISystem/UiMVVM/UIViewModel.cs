using System;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.ProjectLibraries.UISystem.UiMVVM
{
public abstract class UIViewModel: DisposableBase, IUIViewModel
{
    public abstract UIType UIType { get; }
    public abstract event Action<IUIViewModel> ViewShown;
    public abstract event Action<IUIViewModel> ViewHidden;
    public abstract void Init(IUIView view, IAnimatorService animatorService);
    public abstract void Deinit();
    public abstract void ShowView();
    public abstract void ShowHiddenView();
    public abstract void HideView();
    public abstract IInstantiatable GetInstantiatable();
}
}