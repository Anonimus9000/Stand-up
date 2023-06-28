using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Scenes.Home.UIs.MainUIs.MainHome.Components
{
public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image _progress;

    [FormerlySerializedAs("_showAnimationDuration")]
    [SerializeField]
    private float _showDuration;

    [SerializeField]
    private Ease _showEase;

    [SerializeField]
    private float _hideDuration;

    [SerializeField]
    private float _showMoveAnimationDuration;

    [SerializeField]
    private Ease _showMoveEase;

    [SerializeField]
    private Ease _hideEase;

    [SerializeField]
    private float _showMoveStartOffset;

    public event Action ProgressAnimationCompleted;

    private Vector3 _initialScale;
    private Vector3 _initialPosition;
    private Tween _progressFillAmountTween;
    private Tween _hideScaleTween;
    private bool _hideProgressBarOnCompleted;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _initialPosition = transform.position;
    }

    public void ShowProgress(float duration, bool hideProgressBarOnCompleted = true)
    {
        var progressTransform = _progress.transform;

        _hideProgressBarOnCompleted = hideProgressBarOnCompleted;
        
        StartMoveAnimation(progressTransform);
        
        StartShowAnimation(progressTransform);
        
        _progressFillAmountTween?.Kill();
        
        _progressFillAmountTween = _progress.DOFillAmount(0, duration);
        _progressFillAmountTween.onComplete += OnProgressAnimationCompleted;
    }

    public void HideProgressBar()
    {
        StartHideAnimation(_progress.transform);
    }

    public void Pause()
    {
        _progressFillAmountTween.Pause();
    }

    public void Continue()
    {
        _progressFillAmountTween.Play();
    }

    private void StartShowAnimation(Transform progressTransform)
    {
        progressTransform.localScale = Vector3.zero;

        progressTransform.DOScale(_initialScale, _showMoveAnimationDuration).SetEase(_showEase);
    }

    private void StartMoveAnimation(Transform progressTransform)
    {
        var initialPositionY = _initialPosition.y;
        progressTransform.localPosition = new Vector3(_initialPosition.x, _initialPosition.y - _showMoveStartOffset,
            _initialPosition.z);

        progressTransform.DOLocalMove(new Vector3(_initialPosition.x, initialPositionY, _initialPosition.z),
            _showDuration).SetEase(_showMoveEase);
    }

    private void StartHideAnimation(Transform progressTransform)
    {
        _hideScaleTween = progressTransform.DOScale(Vector3.zero, _hideDuration).SetEase(_hideEase);
        _hideScaleTween.onComplete += OnHideAnimationCompleted;
    }

    private void OnHideAnimationCompleted()
    { 
        Destroy(gameObject);
    }

    private void OnProgressAnimationCompleted()
    {
        ProgressAnimationCompleted?.Invoke();

        if (_hideProgressBarOnCompleted)
        {
            HideProgressBar();
        }
    }
}
}