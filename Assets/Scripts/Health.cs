using System;
using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour
{
    [SerializeField] int _health;
    [SerializeField] int _maxHealth;
    [SerializeField] SoundData _deathSound;
    [SerializeField] bool _killSelf;
    [SerializeField] UnityEvent OnDamage;
    [SerializeField] UnityEvent OnDeath;
    public event Action BeforeDeath;
    public event Action<int> HealthUpdate;

    public int MaxHealth => _maxHealth;
    void Start()
    {
        HealthUpdate?.Invoke(_health);

    }
    void FixedUpdate()
    {
        if(_killSelf)
        {
            _killSelf = false;
            Die();
        }
    }
    public void DoDamage(int amount)
    {
        _health -= Mathf.Abs(amount);
        if (_health < 1)
            Die();
        HealthUpdate?.Invoke(_health);
        OnDamage?.Invoke();
    }
    public void Heal(int amount)
    {
        _health += Mathf.Abs(amount);
        if (_health > _maxHealth)
            _health = _maxHealth;
        HealthUpdate?.Invoke(_health);

    }
    public void IncreaseMaxLife(int amount)
    {
        _maxHealth += Mathf.Abs(amount);
    }
    void Die()
    {
        BeforeDeath?.Invoke();
        OnDeath?.Invoke();
        GlobalSounds.Instance.PlaySound(_deathSound);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        BeforeDeath = null;
        HealthUpdate = null;
    }
}
