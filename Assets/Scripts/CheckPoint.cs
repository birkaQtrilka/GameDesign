using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D)), SelectionBase]
public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject activatedVisual;
    [SerializeField] SoundData _sound;
    Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activatedVisual.activeInHierarchy)
        {
            GlobalSounds.Instance.PlaySound(_sound);
            GameManager.Instance.SaveState(transform.position + Vector3.up*0.1f);
            _collider.enabled = false;
            activatedVisual.SetActive(true);
        }
    }
}
