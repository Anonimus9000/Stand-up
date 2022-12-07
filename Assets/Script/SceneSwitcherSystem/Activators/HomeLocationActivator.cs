using Script.SceneSwitcherSystem.Activators.Base;
using UnityEngine;

namespace Script.SceneSwitcherSystem.Activators
{
public class HomeLocationActivator : IActivator
{
    private readonly GameObject _homeGameObject;

    public HomeLocationActivator(GameObject homeGameObject)
    {
        _homeGameObject = homeGameObject;
    }

    public void Activate()
    {
        _homeGameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _homeGameObject.SetActive(false);
    }
}
}