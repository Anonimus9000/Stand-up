using Script.ProjectLibraries.UISystem.Managers.Instantiater;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base;

namespace Script.ProjectLibraries.UISystem.UIWindow
{
public interface IUIView : IInstantiatable, IAnimatable
{
    void OnShown();
    void OnHidden();
}
}