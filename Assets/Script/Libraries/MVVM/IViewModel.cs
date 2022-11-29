namespace Script.Libraries.MVVM
{
public interface IViewModel
{
    IModel Model { get; }
    IView View { get; }
}
}