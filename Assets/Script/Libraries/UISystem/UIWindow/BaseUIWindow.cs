using Script.Libraries.UISystem.Managers.Instantiater;
using UnityEngine;

namespace Script.Libraries.UISystem.UIWindow
{
    public abstract class BaseUIWindow : MonoBehaviour, IInstantiatble
    {
        public virtual void OnShown() { }

        public virtual void OnHidden() { }
    }
}