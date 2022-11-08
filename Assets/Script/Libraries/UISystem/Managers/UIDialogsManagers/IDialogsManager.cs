using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
    public interface IDialogsManager
    {
        void Initialize(string dialogPrefabsPath, IInstantiater instantiater);
        BaseUIWindow Show<T>() where T : BaseUIWindow;
        void Close<T>() where T : BaseUIWindow;
        BaseUIWindow Hide<T>() where T : BaseUIWindow;
    }
}