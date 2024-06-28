using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Jump : MovementState
{
    public Jump(MovementStateMachine context) : base(context) { }
    Vector2 _endPos;
    Vector2 _startPos;
    float _resetTime;
    float _beatTime;
    public override void OnEnter()
    {

        _endPos = context.transform.position + context.JumpHeight * context.transform.up;
        _startPos = context.transform.position;
        _beatTime = 1f / (SongManager.Instance.Song.Bpm / 60f) * context.BeatsForCompletion;
        _resetTime = Time.time;

    }
    public override void Update()
    {
        var normalizedProgress = Mathf.Clamp((Time.time - _resetTime) / _beatTime, 0, 1);
        var easing = context.JumpCurve.Evaluate(normalizedProgress);
        //if(context.gr)
        context.Rigidbody.MovePosition(Vector3.Lerp(_startPos, _endPos, easing));
        if (normalizedProgress == 1)
        {
            context.StartCoroutine(PerformJumpLag());
        }

    }
    IEnumerator PerformJumpLag()
    {
        yield return new WaitForSeconds(context.JumpLag);
        if(context.Break) yield break;
        context.TransitionToState(typeof(Fall));
    }
    
}
