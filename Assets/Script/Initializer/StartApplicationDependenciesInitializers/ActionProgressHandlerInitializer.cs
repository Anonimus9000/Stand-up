using Script.ConfigData.InGameEventsConfig;
using Script.Initializer.Base;
using Script.InteractableObject.ActionProgressSystem.Handler;

namespace Script.Initializer.StartApplicationDependenciesInitializers
{
public class ActionProgressHandlerInitializer : IDependenciesInitializer
{
    private HomeActionProgressHandler _homeActionProgressHandler;
    private readonly InActionProgressEventsConfig _eventsConfig;

    public ActionProgressHandlerInitializer(InActionProgressEventsConfig eventsConfig)
    {
        _eventsConfig = eventsConfig;
    }
    
    public IInitializable Initialize()
    {
        _homeActionProgressHandler = new HomeActionProgressHandler(_eventsConfig);

        return _homeActionProgressHandler;
    }
}
}