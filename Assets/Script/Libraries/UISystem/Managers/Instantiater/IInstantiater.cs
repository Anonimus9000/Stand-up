namespace Script.Libraries.UISystem.Managers.Instantiater
{
public interface IInstantiater
{
    public IInstantiatable Instantiate(IInstantiatable instantiatable);
    public void Destroy(IInstantiatable instantiatable);

    public void SetActive(IInstantiatable popupDialog, bool isActive);
}
}