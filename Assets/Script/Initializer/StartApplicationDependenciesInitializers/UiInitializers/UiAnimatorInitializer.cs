using DG.Tweening;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider;
using Script.Libraries.UISystem.Managers.UiAnimatorServiceProvider.Base;
using Script.UI.AnimatorServiceProvider.Services;
using UnityEngine;

namespace Script.Initializer.StartApplicationDependenciesInitializers.UiInitializers
{
public class UiAnimatorInitializer : MonoBehaviour
{
    [Header("Popup animator settings")]
    [SerializeField]
    private Ease _showPopupFadeEase = Ease.Linear;
    [SerializeField]
    private Ease _hidePopupFadeEase = Ease.Linear;
    [SerializeField]
    private Ease _showPopupScaleEase = Ease.Linear;
    [SerializeField]
    private Ease _hidePopupScaleEase = Ease.Linear;

    [SerializeField]
    private float _showPopupFadeDuration = 0.3f;
    [SerializeField]
    private float _hidePopupFadeDuration = 0.3f;
    [SerializeField]
    private float _showPopupScaleDuration = 0.3f;
    [SerializeField]
    private float _hidePopupScaleDuration = 0.3f;
    
    [Space(5)]
    [Header("FullScreen animator settings")]
    [SerializeField]
    private Ease _showFullScreenFadeEase = Ease.Linear;
    [SerializeField]
    private Ease _hideFullScreenFadeEase = Ease.Linear;
    [SerializeField]
    private Ease _showFullScreenScaleEase = Ease.Linear;
    [SerializeField]
    private Ease _hideFullScreenScaleEase = Ease.Linear;

    [SerializeField]
    private float _showFullScreenFadeDuration = 0.3f;
    [SerializeField]
    private float _hideFullScreenFadeDuration = 0.3f;
    [SerializeField]
    private float _showFullScreenScaleDuration = 0.3f;
    [SerializeField]
    private float _hideFullScreenScaleDuration = 0.3f;
    
    [Space(5)]
    [Header("MainUi animator settings")]
    [SerializeField]
    private Ease _showMainUiFadeEase = Ease.Linear;
    [SerializeField]
    private Ease _hideMainUiFadeEase = Ease.Linear;
    [SerializeField]
    private Ease _showMainUiScaleEase = Ease.Linear;
    [SerializeField]
    private Ease _hideMainUiScaleEase = Ease.Linear;

    [SerializeField]
    private float _showMainUiFadeDuration = 0.3f;
    [SerializeField]
    private float _hideMainUiFadeDuration = 0.3f;
    [SerializeField]
    private float _showMainUiScaleDuration = 0.3f;
    [SerializeField]
    private float _hideMainUiScaleDuration = 0.3f;

    public IAnimatorServiceProvider InitializeAnimatorServiceProvider()
    {
        var popupAnimatorServiceBehaviour = new PopupAnimatorServiceBehaviour(
            _showPopupFadeEase,
            _hidePopupFadeEase,
            _showPopupScaleEase,
            _hidePopupScaleEase,
            _showPopupFadeDuration,
            _hidePopupFadeDuration,
            _showPopupScaleDuration,
            _hidePopupScaleDuration
        );
        
        var fullScreenAnimatorServiceBehaviour = new FullScreenAnimatorServiceBehaviour(
            _showFullScreenFadeEase,
            _hideFullScreenFadeEase,
            _showFullScreenScaleEase,
            _hideFullScreenScaleEase,
            _showFullScreenFadeDuration,
            _hideFullScreenFadeDuration,
            _showFullScreenScaleDuration,
            _hideFullScreenScaleDuration
            );
        
        var mainUiAnimatorServiceBehaviour = new MainUiAnimatorServiceBehaviour(
            _showMainUiFadeEase,
            _hideMainUiFadeEase,
            _showMainUiScaleEase,
            _hideMainUiScaleEase,
            _showMainUiFadeDuration,
            _hideMainUiFadeDuration,
            _showMainUiScaleDuration,
            _hideMainUiScaleDuration
        );

        var animatorServiceProvider = new AnimatorServiceProvider(
            popupAnimatorServiceBehaviour,
            fullScreenAnimatorServiceBehaviour,
            mainUiAnimatorServiceBehaviour);

        return animatorServiceProvider;
    }
}
}