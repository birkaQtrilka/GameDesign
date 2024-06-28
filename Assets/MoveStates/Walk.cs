using System;
using Unity.VisualScripting;
using UnityEngine;

public class Walk : MovementState
{
    public Walk(MovementStateMachine context) : base(context) { }
    Vector3 _startPos;
    Vector3 _endPos;
    float _resetTime;
    float _beatTime;
    public override void OnEnter()
    {
        var dir = context.DirectionInput.x * context.transform.right.x < 0 ? -1 : 1;
        context.transform.right *= dir;
        Vector3 contextPosition = context.transform.position;
        Vector3 contextRight = context.transform.right;

        _endPos = contextPosition + context.WalkStep * contextRight;
        _startPos = context.transform.position;
        _beatTime = 1f / (SongManager.Instance.Song.Bpm / 60f) * context.BeatsForCompletion;
        _resetTime = Time.time;

        var rayStart = contextPosition + contextRight * 0.6f;//0.6f == half size of context size + 0.1f
        var hit = Physics2D.Raycast(rayStart, contextRight, context.WalkStep - .6f, ~(1 << context.gameObject.layer) & ~LayerMask.GetMask("GroundOnly")) ;
        
        if (hit.collider != null && !hit.collider.isTrigger)
        {
            context.TransitionToState(typeof(Idle));
            
        }
        
    }
    public override void Update()
    {
        var normalizedProgress = Mathf.Clamp((Time.time - _resetTime) / _beatTime, 0, 1);
        var easing = context.WalkCurve.Evaluate(normalizedProgress);
       
        context.Rigidbody.MovePosition(Vector3.Lerp(_startPos, _endPos, easing));

        if (normalizedProgress == 1)
        {
            if (!context.Grounded)
                context.TransitionToState(typeof(Fall));
            else
                context.TransitionToState(typeof(Idle));
        }
        

    }
    

}