using System.Collections.Generic;
using Script.Libraries.UISystem.UIWindow;

namespace Script.Libraries.UISystem.Managers.UIWindowsLoader
{
public interface IWindowsLoader
{
    public List<IUIView> UIWindows { get; }

    void LoadDialogs(string pathToLoad);
}
}