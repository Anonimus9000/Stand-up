using System.Collections.Generic;
using Script.Libraries.UISystem.Managers.Instantiater;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIDialogsManagers
{
    public interface IDialogsManager
    {
        void Initialize(IInstantiater instantiater, List<IUIWindow> windows, UIManager uiManager);
        
        IUIWindow Show<T>() where T : IUIWindow;
        void Close<T>() where T : IUIWindow;
    }
}