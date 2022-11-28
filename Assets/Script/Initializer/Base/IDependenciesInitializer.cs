namespace Script.Initializer.Base
{
public interface IDependenciesInitializer : IInitializer
{
    IInitializable Initialize();
}
}