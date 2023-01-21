using Script.ConfigData.InGameEventsConfig;
using Script.Initializer.Base;
using Script.InteractableObject.ActionProgressSystem.Handler;

namespace Script.Initializer.StartApplicationDependenciesInitializers
{
public class ActionProgressHandlerInitializer : IDependenciesInitializer
{
    private HomeActionProgressHandler _homeActionProgressHandler;
    private InActionProgressConfig _config;

    public void SetDependencies(InActionProgressConfig config)
    {
        _config = config;
    }
    
    public IInitializable Initialize()
    {
        _homeActionProgressHandler = new HomeActionProgressHandler(_config);

        return _homeActionProgressHandler;
    }
}
}