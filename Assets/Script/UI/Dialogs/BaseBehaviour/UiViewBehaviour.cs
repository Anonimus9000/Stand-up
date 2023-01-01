using System;
using Script.Libraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.UI.Dialogs.BaseBehaviour
{
[RequireComponent(typeof(CanvasGroup))]
public abstract class UiViewBehaviour : MonoBehaviour, IUIView
{
    public abstract void OnShown();
    public abstract void OnHidden();
}
}