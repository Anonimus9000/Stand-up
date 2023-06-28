using System;
using DG.Tweening;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base;
using Script.ProjectLibraries.UISystem.Managers.UiAnimatorServiceProvider.Base.Animators;
using UnityEngine;

namespace Script.UI.AnimatorServiceProvider.Services
{
public class FullScreenAnimatorServiceBehaviour : IFullScreenAnimatorService
{
    public event Action ShowCompleted;
    public event Action HideCompleted;
    
    private readonly Ease _showFadeEase;
    private readonly Ease _hideFadeEase;
    private readonly Ease _showScaleEase;
    private readonly Ease _hideScaleEase;
    
    private readonly float _showFadeDuration;
    private readonly float _hideFadeDuration;
    private readonly float _showScaleDuration;
    private readonly float _hideScaleDuration;
    
    private Tween _hideFadeTween;
    private Tween _showFadeTween;
    private Tween _hideScaleTween;
    private Tween _showScaleTween;
    private readonly Vector3 _initialScaleView = new(1, 1, 1);

    public FullScreenAnimatorServiceBehaviour(
        Ease showFadeEase,
        Ease hideFadeEase,
        Ease showScaleEase,
        Ease hideScaleEase,
        float showFadeDuration,
        float hideFadeDuration,
        float showScaleDuration,
        float hideScaleDuration)
    {
        _showFadeEase = showFadeEase;
        _hideFadeEase = hideFadeEase;
        _showScaleEase = showScaleEase;
        _hideScaleEase = hideScaleEase;
        _showFadeDuration = showFadeDuration;
        _hideFadeDuration = hideFadeDuration;
        _showScaleDuration = showScaleDuration;
        _hideScaleDuration = hideScaleDuration;
    }
    
    public void StartShowAnimation(IAnimatable animatable)
    {
        if (animatable is MonoBehaviour monoBehaviour)
        {
            var canvasGroup = monoBehaviour.GetComponent<CanvasGroup>();
            var rectTransform = monoBehaviour.transform as RectTransform;

            StartShowFadeAnimation(canvasGroup, _showFadeDuration);
            StartShowScaleAnimation(rectTransform, _showScaleDuration);
        }
        else
        {
            throw new Exception("Popup service can work only with MonoBehaviour");
        }
    }

    public void StartHideAnimation(IAnimatable animatable)
    {
        if (animatable is MonoBehaviour monoBehaviour)
        {
            var canvasGroup = monoBehaviour.GetComponent<CanvasGroup>();
            var rectTransform = monoBehaviour.transform as RectTransform;

            StartHideFadeAnimation(canvasGroup, _hideFadeDuration);
            StartHideScaleAnimation(rectTransform, _initialScaleView, _hideScaleDuration);
        }
        else
        {
            throw new Exception("Popup service can work only with MonoBehaviour");
        }
    }

    private void StartShowScaleAnimation(Transform rectTransform, float duration)
    {
        var initialScale = rectTransform.localScale;
        rectTransform.localScale = Vector3.zero;

        _showScaleTween?.Kill();
        _showScaleTween = rectTransform.DOScale(initialScale, duration);
        _showScaleTween.SetEase(_showScaleEase);
        _showScaleTween.onComplete += OnShowAnimationCompleted;
    }

    private void StartHideScaleAnimation(Transform rectTransform, Vector3 initialScale, float duration)
    {
        rectTransform.localScale = initialScale;

        _hideScaleTween?.Kill(true);
        _hideScaleTween = rectTransform.DOScale(Vector3.zero, duration);
        _hideScaleTween.SetEase(_hideScaleEase);
        _hideScaleTween.onComplete += OnHideAnimationCompleted;
    }

    private void StartShowFadeAnimation(CanvasGroup canvasGroup, float duration)
    {
        _showFadeTween?.Kill();
        canvasGroup.alpha = 0;
        _showFadeTween = canvasGroup.DOFade(1, duration);
        _showFadeTween.SetEase(_showFadeEase);
    }

    private void StartHideFadeAnimation(CanvasGroup canvasGroup, float duration)
    {
        _hideFadeTween?.Kill(true);
        canvasGroup.alpha = 1;
        _hideFadeTween = canvasGroup.DOFade(0, duration);
        _hideFadeTween.SetEase(_hideFadeEase);
    }

    private void OnShowAnimationCompleted()
    {
        ShowCompleted?.Invoke();
    }

    private void OnHideAnimationCompleted()
    {
        HideCompleted?.Invoke();
    }
}
}