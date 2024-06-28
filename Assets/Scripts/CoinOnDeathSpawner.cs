using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(Health))]
public class CoinOnDeathSpawner : MonoBehaviour
{
    [SerializeField] int _amount;
    [SerializeField] float _rangeOfDrop = .5f;
    [SerializeField] Rigidbody2D _coinPrefab;
    [SerializeField] float _upwardsForceMin;
    [SerializeField] float _upwardsForceMax;
    Health _health;
    
    void Awake()
    {
        _health = GetComponent<Health>();
    }
    void OnEnable()
    {
        _health.BeforeDeath += OnDeath;
    }
    void OnDisable()
    {
        _health.BeforeDeath -= OnDeath;

    }
    void OnDeath()
    {
        for(int i = 0; i < _amount; i++) 
        {
            var coinRb = Instantiate(_coinPrefab);
            coinRb.transform.position = transform.position;
            var a =  transform.right * _rangeOfDrop / 2;
            var randRange = Vector3.Slerp(transform.up - a, transform.up + a, Random.Range(0f, 1f)).normalized * Random.Range(_upwardsForceMin, _upwardsForceMax);
            coinRb.AddForce(randRange, ForceMode2D.Impulse);
        }
    }
}
