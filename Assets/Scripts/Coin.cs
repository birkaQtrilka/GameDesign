using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[SelectionBase]
public class Coin : PickUp
{
    [SerializeField] GameObject _destroyOnContact;
    [SerializeField] SpriteRenderer _visual;
    [SerializeField] float _fadingDistanceMin;
    [SerializeField] float _fadingDistanceMax;
    [SerializeField] float _fadingDuration;
    Collider2D _collider;
    Sequence sequence;
    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        if (_destroyOnContact == null)
            _destroyOnContact = gameObject;
        
    }
    
    public override void Grab(GameObject player)
    {
        GameManager.Instance.CoinAmount++;

        _collider.enabled = false;

        sequence = DOTween.Sequence();
        sequence.Insert(0,_destroyOnContact.transform.DOMove(_destroyOnContact.transform.position + transform.up * Random.Range(_fadingDistanceMin, _fadingDistanceMax), _fadingDuration));
        sequence.Insert(0,_visual.DOFade(0, _fadingDuration));
        sequence.onComplete = () => Destroy(_destroyOnContact);

    }

    private void OnDisable()
    {
        sequence?.Kill();
    }

    
}
