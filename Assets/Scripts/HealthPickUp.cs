using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[SelectionBase]
public class HealthPickUp :  PickUp
{
    [SerializeField] int _healAmount;
    [SerializeField] TextMeshPro _textMeshPrefab;
    [SerializeField] float _popUpDuration;
    public override void Grab(GameObject player)
    {
        player.GetComponent<Health>().Heal(_healAmount);
        var inst = Instantiate(_textMeshPrefab);
        inst.transform.position = transform.position;
        inst.text = $"+{_healAmount} HP";
        inst.transform.DOMove(Vector3.up + transform.position, _popUpDuration);
        inst.DOFade(0, _popUpDuration);
        Destroy(gameObject);
    }

    
}
