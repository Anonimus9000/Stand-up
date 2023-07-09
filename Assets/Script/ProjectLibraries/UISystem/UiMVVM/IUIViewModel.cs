using System;
using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.ProjectLibraries.UISystem.UiMVVM
{
public interface IUIViewModel: IViewModel
{
    public UIType UIType { get; }
    event Action<IUIViewModel> ViewShown;
    event Action<IUIViewModel> ViewHidden;
    
    //TODO: Check downcast in the profiler for spikes in memory allocation.
    public void Init(IUIView view, IAnimatorService animatorService);
    public void Deinit();
    public void ShowView();
    public void ShowHiddenView();
    public void HideView();
    public IInstantiatable GetInstantiatable();
}
}