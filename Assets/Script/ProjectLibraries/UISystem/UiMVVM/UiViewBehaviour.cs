using Script.ProjectLibraries.MVVM;
using Script.ProjectLibraries.UISystem.UIWindow;
using UnityEngine;

namespace Script.ProjectLibraries.UISystem.UiMVVM
{
[RequireComponent(typeof(CanvasGroup))]
public abstract class UiViewBehaviour : BehaviourDisposableBase, IUIView
{
    public abstract void OnShown();
    public abstract void OnHidden();
}
}