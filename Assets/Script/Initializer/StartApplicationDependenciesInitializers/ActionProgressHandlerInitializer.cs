using Script.Initializer.Base;
using Script.InteractableObject.ActionProgressSystem;

namespace Script.Initializer.StartApplicationDependenciesInitializers
{
public class ActionProgressHandlerInitializer : IDependenciesInitializer
{
    private HomeActionProgressHandler _homeActionProgressHandler;
    public IInitializable Initialize()
    {
        _homeActionProgressHandler = new HomeActionProgressHandler();

        return _homeActionProgressHandler;
    }
}
}