namespace Script.ProjectLibraries.ConfigParser.Base
{
public interface IInActionProgressConfig : IConfig
{
    public int ChangeToShowEvent { get; }
    public int MinProgressPercentToShowEvent { get; }
    public int MaxProgressPercentToShowEvent { get; }
    public float HowOftenTryShowEventPerSecond { get; }
}
}