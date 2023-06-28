using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;

namespace Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base
{
public interface IAnimatorServiceProvider
{
    T GetService<T>() where T : IAnimatorService;
}
}