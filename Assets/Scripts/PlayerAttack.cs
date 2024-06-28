using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using Unity.VisualScripting;
public interface IAttacker
{
    void Attack();
}
[RequireComponent(typeof(AudioSource))]
public class PlayerAttack : MonoBehaviour, IAttacker
{
    [SerializeField] int _damageAmount;
    [SerializeField] float _criticalMultiplier;
    [SerializeField] Sprite _attackSprite;
    [SerializeField] Sprite _criticalAttackSprite;
    [SerializeField] float _spriteStayTime;
    [SerializeField] AudioClip _attackAudioClip;
    [SerializeField] SpriteRenderer _attackRenderer;
    [SerializeField] ContactFilter2D _attackFilter;
    [SerializeField] UnityEvent OnAttack;
    [SerializeField] UnityEvent OnCrit;

    public float AttackCooldown { get => _spriteStayTime; set=>_spriteStayTime = value; }

    readonly Collider2D[] _attackContacts = new Collider2D[4];
    bool _canAttack = true;
    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _attackRenderer.transform.SetParent(null);
    }
    
    public void Attack()
    {
        int damage;
        Sprite selectedSprite;
        if (SongManager.Instance.OnBeatMargin)
        {
            selectedSprite = _criticalAttackSprite;
            damage = Mathf.FloorToInt(_damageAmount * _criticalMultiplier);
            OnCrit?.Invoke();

        }
        else
        {
            selectedSprite = _attackSprite;
            damage = _damageAmount;
        }

        _attackRenderer.sprite = selectedSprite;
        var spritePos = transform.position + (_attackRenderer.size.x/2) * transform.right;
        _attackRenderer.transform.right = transform.right;
        _attackRenderer.transform.position = spritePos;
        Physics2D.OverlapBox(spritePos, _attackRenderer.size, 0, _attackFilter, _attackContacts);
        for(int i = 0; i< _attackContacts.Length; i++) 
        {
            var c = _attackContacts[i];
            if (c == null) continue;
            if (c.TryGetComponent<Health>(out var healthHandler))
                healthHandler.DoDamage(damage);
            _attackContacts[i] = null;
        }
        OnAttack?.Invoke();
        _attackRenderer.gameObject.SetActive(true);

        StartCoroutine(SpriteCooldown());
    }

    public void SetCritMult(float mult, Sprite visual)
    {
        _criticalAttackSprite = visual;
        _criticalMultiplier = mult;
    }

    IEnumerator SpriteCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_spriteStayTime);
        _attackRenderer.gameObject.SetActive(false);
        _canAttack = true;
    }
    public void PlaySound()
    {
        _audioSource.clip = _attackAudioClip;
        _audioSource.Play();
    }
    void Update()
    {
        if (_canAttack && Input.GetKeyDown(KeyCode.Space)) 
        {
            Attack();    
        }
    }
}
