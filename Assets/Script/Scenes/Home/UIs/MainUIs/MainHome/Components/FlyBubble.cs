using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Scenes.Home.UIs.MainUIs.MainHome.Components
{
public class FlyBubble : MonoBehaviour
{
    [SerializeField]
    private Image _bubbleImage;

    [SerializeField]
    private TextMeshProUGUI _bubbleBodyInfo;

    [SerializeField]
    private int _yOffsetOnShow;

    [SerializeField]
    private float _showDuration;

    [SerializeField]
    private float _moveDuration;

    [SerializeField]
    private Ease _showEase;

    [SerializeField]
    private Ease _moveEase;

    public event Action<FlyBubble> MoveCompleted;
    public int BodyInfo { get; private set; }

    private Tween _showTween;
    private Vector3 _endPosition;
    private Tween _moveTween;

    public void ShowAndMoveBubble(Vector3 startPosition, Vector3 endPosition, int bodyInfo)
    {
        BodyInfo = bodyInfo;
        _bubbleBodyInfo.text = bodyInfo.ToString();

        _showTween?.Kill();

        _bubbleImage.transform.localPosition = startPosition;
        _endPosition = endPosition;

        var endShowPosition = new Vector3(startPosition.x, startPosition.y + _yOffsetOnShow, startPosition.z);
        _showTween = _bubbleImage.transform.DOLocalMove(endShowPosition, _showDuration).SetEase(_showEase);
        _showTween.onComplete += OnShowCompleted;
    }

    //TODO: add animation destroy
    public void Destroy()
    {
        _bubbleImage.transform.DOScale(Vector3.zero, 0.3f).onComplete += () => Destroy(gameObject);

        MoveCompleted = null;
    }

    private void OnShowCompleted()
    {
        _moveTween?.Kill();

        _moveTween = _bubbleImage.transform.DOMove(_endPosition, _moveDuration).SetEase(_moveEase);
        _moveTween.onComplete += OnMoveCompleted;
    }

    private void OnMoveCompleted()
    {
        MoveCompleted?.Invoke(this);
    }
}
}