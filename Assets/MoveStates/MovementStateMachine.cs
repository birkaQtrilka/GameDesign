using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Linq;
public abstract class MovementStateMachine : MonoBehaviour
{
    public Dictionary<Type, MovementState> States;
    public MovementState CurrentState { get; protected set; }
    protected bool _transitioning;
    readonly Stack<Collider2D> _colliders = new();
    readonly Stack<Collider2D> _collidersHead = new();

    [SerializeField] float _walkStep;
    [SerializeField] AnimationCurve _walkCurve;
    [SerializeField] float _beatsForCompletion;
    [SerializeField] float _jumpLag;
    [SerializeField] float _jumpHeight;
    [SerializeField] AnimationCurve _jumpCurve;
    [SerializeField] AnimationCurve _fallCurve;
    [SerializeField] float _maxFallSpeed;
    [SerializeField] float _fallAccelerationBuildUpSpeed;
    [SerializeField] TriggerDelegator _groundHitDelegator;
    public float WalkStep { get { return _walkStep; } }
    public AnimationCurve WalkCurve { get { return _walkCurve; } }
    public float BeatsForCompletion { get { return _beatsForCompletion; } }
    public float JumpHeight { get { return _jumpHeight; } }
    public AnimationCurve JumpCurve { get { return _jumpCurve; } }
    public AnimationCurve FallCurve { get { return _fallCurve; } }
    public float MaxFallSpeed { get { return _maxFallSpeed; } }
    public float FallAccelerationBuildUpSpeed { get { return _fallAccelerationBuildUpSpeed; } }
    public bool Grounded => GroundHitInfo != null;
    public Vector2 DirectionInput { get; protected set; }
    public Rigidbody2D Rigidbody { get; protected set; }
    public float JumpLag { get { return _jumpLag; } }
    public Vector2 ParentDelta { get; set; }
    bool _break;
    public bool Break
    {
        get => _break;
        set
        {
            if (value)
                TransitionToState(typeof(Idle));
            _break = value;
        }
            
    }
    public Collider2D GroundHitInfo 
    { 
        get 
        {
            _colliders.TryPeek(out var coll);
            return coll; 
        } 
    }

    public void SetDirection(Vector2 direction)
    {
        DirectionInput = direction;
    }
    protected virtual void OnEnable()
    {
        _groundHitDelegator.OnTriggerEnter += OnColliderHit;
        _groundHitDelegator.OnTriggerExit += OnColliderExit;
    }
    protected virtual void OnDisable()
    {
        _groundHitDelegator.OnTriggerEnter -= OnColliderHit;
        _groundHitDelegator.OnTriggerExit -= OnColliderExit;
    }
    public void TransitionToState(Type stateType)
    {
        _transitioning = true;
        CurrentState.OnExit();
        CurrentState = States[stateType];

        //grid locking
        var pos = transform.position;
        var sign = pos.x < 0 ? -1 : 1;
        transform.position = new((Mathf.Floor(Mathf.Abs(pos.x)) + 0.5f) *sign, pos.y, pos.z);

        CurrentState.OnEnter();
        _transitioning = false;
    }
    //public override void OnExit()
    //{
    //    //grid locking
    //    var pos = context.transform.position;
    //    context.transform.position = new(Mathf.Floor(pos.x) + 0.5f * context.transform.right.x, pos.y, pos.z);
    //}
    void OnColliderHit(Collider2D col)
    {
        //The layer of ground checker object should be set to "GroundChecker"
        _colliders.Push(col);
    }
    void OnColliderExit(Collider2D col)
    {
        _colliders.Pop();
    }
    //bool _hitHead;
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    List<ContactPoint2D> points = new();
    //    collision.GetContacts(points);
    //    if (points.Any(p => p.normal.y < 0))
    //        _hitHead = true;

    //}
}