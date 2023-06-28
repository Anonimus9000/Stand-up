using System;

namespace Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators
{
public interface IAnimatorService
{
    event Action ShowCompleted;
    event Action HideCompleted;
    
    void StartShowAnimation(IAnimatable animatable);
    void StartHideAnimation(IAnimatable animatable);
}
}