using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDamageable
{
    void DoDamage(int amount);
}

//[Serializable] public class MovementDict : SerializableDictionary<Direction, MoveAmount> { }

public class Enemy : MovementStateMachine
{

    [SerializeField] List<Act> _actPattern;
    int _movePatternIndex;
    void Awake()
    {
        States = new()
        {
            { typeof(Idle), new Idle(this) },
            { typeof(Walk), new Walk(this) },
            { typeof(Jump), new Jump(this) },
            { typeof(Fall), new Fall(this) }
        };
        Rigidbody = GetComponent<Rigidbody2D>();

        CurrentState = States[typeof(Idle)];
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        SongManager.Instance.Beat += OnBeat;

    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if(SongManager.Instance != null)
            SongManager.Instance.Beat -= OnBeat;

    }
    void FixedUpdate()
    {
        if (_transitioning) return;
        CurrentState.Update();
        Rigidbody.velocity = Vector3.zero;
    }
    void OnBeat()
    {
        if(_actPattern.Count != 0)
            _actPattern[_movePatternIndex].Perform(this);
        if(++_movePatternIndex >= _actPattern.Count)
            _movePatternIndex = 0;
        StartCoroutine(ZeroAfterFrame());
    }
    IEnumerator ZeroAfterFrame()
    {
        yield return new WaitForFixedUpdate();
        DirectionInput = Vector2.zero;
    }
}
