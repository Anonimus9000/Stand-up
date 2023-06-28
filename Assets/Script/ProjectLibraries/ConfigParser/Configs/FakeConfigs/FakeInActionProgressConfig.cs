using Script.ProjectLibraries.ConfigParser.Base;

namespace Script.ProjectLibraries.ConfigParser.Configs.FakeConfigs
{
public class FakeInActionProgressConfig : IInActionProgressConfig
{
    public int ChangeToShowEvent { get; }
    public int MinProgressPercentToShowEvent { get; }
    public int MaxProgressPercentToShowEvent { get; }
    public float HowOftenTryShowEventPerSecond { get; }

    public FakeInActionProgressConfig(int changeToShowEvent, int minProgressPercentToShowEvent, int maxProgressPercentToShowEvent, float howOftenTryShowEventPerSecond)
    {
        ChangeToShowEvent = changeToShowEvent;
        MinProgressPercentToShowEvent = minProgressPercentToShowEvent;
        MaxProgressPercentToShowEvent = maxProgressPercentToShowEvent;
        HowOftenTryShowEventPerSecond = howOftenTryShowEventPerSecond;
    }
}
}