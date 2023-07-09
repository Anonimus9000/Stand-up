using Script.ProjectLibraries.UISystem.UiMVVM;
using Script.ProjectLibraries.UISystem.UIWindow;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.CommonUIs.PopupDialogs.InGameEvent
{
public class InGameEventView : UiViewBehaviour, IPopup
{
    [SerializeField]
    private Button _acceptButton;

    [SerializeField]
    private Button _rejectButton;

    [SerializeField]
    private Button _closeButton;

    public Button AcceptButton => _acceptButton;
    public Button RejectButton => _rejectButton;
    public Button CloseButton => _closeButton;
    
    public override void OnShown()
    {
    }

    public override void OnHidden()
    {
    }
}
}