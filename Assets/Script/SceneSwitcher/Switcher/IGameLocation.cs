namespace Script.SceneSwitcher.Switcher
{
public interface IGameLocation
{
    void Initialize();
    void Open();
    void Close();
    void OnOpen();
    void OnClose();
}
}