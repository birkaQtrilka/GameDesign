using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pickuper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PickUp>(out var pickUp))
        {
            GlobalSounds.Instance.PlaySound(pickUp.Sound);
            pickUp.Grab(gameObject);

        }
    }
}
