namespace Script.SceneSwitcherSystem.Switcher
{
public interface IGameScene
{
    void Initialize();
    void Open();
    void Close();
    void OnOpen();
    void OnClose();
}
}