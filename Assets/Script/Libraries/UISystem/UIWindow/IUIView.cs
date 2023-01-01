using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base;

namespace Script.Libraries.UISystem.UIWindow
{
public interface IUIView : IInstantiatable, IAnimatable
{
    void OnShown();
    void OnHidden();
}
}