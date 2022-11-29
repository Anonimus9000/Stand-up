namespace Script.Libraries.MVVM
{
public interface IView
{
    IViewModel ViewModel { get; }
    void Initialize();
}
}