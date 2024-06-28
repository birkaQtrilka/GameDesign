using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] float _sizeAddition;
    [SerializeField] float _animDuration;
    [SerializeField] Ease _easing;
    [SerializeField] Ease _easing2;
    Tween _tween;

    public void Scale()
    {
        float copy = transform.localScale.x;
        transform.DOScale(copy + _sizeAddition, _animDuration).SetEase(_easing).OnComplete(() => transform.DOScale(copy, _animDuration).SetEase(_easing2));
    }

    private void OnDisable()
    {
        DOTween.KillAll(transform);
    }
}
