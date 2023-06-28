using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.UI.CommonUIs.BaseBehaviour
{
[RequireComponent(typeof(CanvasGroup))]
public abstract class UiViewBehaviour : BehaviourDisposableBase, IUIView
{
    public abstract void OnShown();
    public abstract void OnHidden();
}
}