using System.Collections.Generic;
using Script.ProjectLibraries.UISystem.UIWindow;

namespace Script.ProjectLibraries.UISystem.Managers.UIWindowsLoader
{
public interface IWindowsLoader
{
    public List<IUIView> UIWindows { get; }

    void LoadDialogs(string pathToLoad);
}
}