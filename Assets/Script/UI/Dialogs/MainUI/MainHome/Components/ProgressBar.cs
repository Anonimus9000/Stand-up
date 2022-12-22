using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.UI.Dialogs.MainUI.MainHome.Components
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

    private Tween _progressFillAmountTween;

    public event Action ProgressAnimationCompleted;

    private Vector3 _initialScale;
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _initialPosition = transform.position;
    }

    public void ShowProgress(float duration)
    {
        var progressTransform = _progress.transform;
        
        StartMoveAnimation(progressTransform);
        
        StartShowAnimation(progressTransform);
        
        _progressFillAmountTween?.Kill();
        
        _progressFillAmountTween = _progress.DOFillAmount(0, duration);
        _progressFillAmountTween.onComplete += OnProgressAnimationCompleted;
    }

    public void HideProgressBar()
    {
        Destroy(gameObject);
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

        progressTransform.DOScale(_initialScale, _showDuration).SetEase(_showEase);
    }

    private void StartMoveAnimation(Transform progressTransform)
    {
        var initialPositionY = _initialPosition.y;
        progressTransform.position = new Vector3(_initialPosition.x, _initialPosition.y - _showMoveStartOffset,
            _initialPosition.z);

        progressTransform.DOMove(new Vector3(_initialPosition.x, initialPositionY, _initialPosition.z),
            _showDuration).SetEase(_showMoveEase);
    }

    private void OnProgressAnimationCompleted()
    {
        ProgressAnimationCompleted?.Invoke();
    }
}
}