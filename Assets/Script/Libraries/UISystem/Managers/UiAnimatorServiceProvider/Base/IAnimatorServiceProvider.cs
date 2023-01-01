using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;

namespace Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base
{
public interface IAnimatorServiceProvider
{
    T GetService<T>() where T : IAnimatorService;
}
}