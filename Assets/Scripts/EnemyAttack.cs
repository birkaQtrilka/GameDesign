using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(AudioSource))]
public class EnemyAttack : MonoBehaviour, IAttacker
{
    [SerializeField] int _damageAmount;
    [SerializeField] Sprite _attackSprite;
    [SerializeField] float _spriteStayTime;

    [SerializeField] SpriteRenderer _attackRenderer;
    [SerializeField] AudioClip _attackAudioClip;
    [SerializeField] UnityEvent OnAttack;
    [SerializeField] ContactFilter2D _attackFilter;
    EmptyMono _rendererCoroutineStarter;
    AudioSource _audioSource;
    readonly Collider2D[] _attackContacts = new Collider2D[3];
    void Awake()
    {
        _rendererCoroutineStarter = _attackRenderer.gameObject.AddComponent<EmptyMono>();
        _audioSource = GetComponent<AudioSource>();
        _attackRenderer.transform.SetParent(null);
    }
    public void Attack()
    {

        _attackRenderer.sprite = _attackSprite;
        var spritePos = transform.position + (_attackRenderer.size.x / 2 ) * transform.right;
        _attackRenderer.transform.right = transform.right;
        _attackRenderer.transform.position = spritePos;
        Physics2D.OverlapBox(spritePos, _attackRenderer.size, 0, _attackFilter, _attackContacts);
        
        for (int i = 0; i < _attackContacts.Length; i++)
        {
            var c = _attackContacts[i];
            if (c == null) continue;
            if (c.TryGetComponent<Health>(out var healthHandler))
            {
                healthHandler.DoDamage(_damageAmount);
            }
            _attackContacts[i] = null;
        }
        //PlaySound();
        OnAttack?.Invoke();
        _attackRenderer.gameObject.SetActive(true);

        _rendererCoroutineStarter.StartCoroutine(SpriteCooldown());
        
    }
    public void PlaySound()
    {
        _audioSource.clip = _attackAudioClip;
        _audioSource.Play();
    }
    IEnumerator SpriteCooldown()
    {
        yield return new WaitForSeconds(_spriteStayTime);
        _attackRenderer.gameObject.SetActive(false);
    }
}

