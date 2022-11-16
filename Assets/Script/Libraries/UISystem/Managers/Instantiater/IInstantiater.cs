namespace Script.Libraries.UISystem.Managers.Instantiater
{
public interface IInstantiater
{
    public IInstantiatble Instantiate(IInstantiatble instantiatble);
    public void Destroy(IInstantiatble instantiatble);

    public void SetActive(IInstantiatble popupDialog, bool isActive);
}
}