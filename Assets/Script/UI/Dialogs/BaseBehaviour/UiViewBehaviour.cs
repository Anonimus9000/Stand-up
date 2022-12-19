using System;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.UI.Dialogs.BaseBehaviour
{
public abstract class UiViewBehaviour : MonoBehaviour, IUIView
{
    public abstract event Action ViewShown;
    public abstract event Action ViewHidden;
    
    public abstract void Show();
    public abstract void Hide();
}
}