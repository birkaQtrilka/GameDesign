using UnityEngine;

public class Fall : MovementState
{

    public Fall(MovementStateMachine context) : base(context) { }
    float _normalizedProgress;
    public override void OnEnter()
    {
        _normalizedProgress = 0;
        //~(1 << context.gameObject.layer)
    }
    public override void Update()//how can I relate falling to bpm?
    {
        _normalizedProgress += context.FallAccelerationBuildUpSpeed * Time.deltaTime;
        if (_normalizedProgress > 1) _normalizedProgress = 1;

        var dir = -context.transform.up;
        var buildUp = context.FallCurve.Evaluate(_normalizedProgress);

        var pos = context.transform.position + buildUp * context.MaxFallSpeed * Time.deltaTime * dir;
        context.Rigidbody.MovePosition(pos);
        if (context.Grounded)
        {
            context.TransitionToState(typeof(Idle));
        }
    }



    //public Fall(MovementStateMachine context) : base(context) { }
    ////float _normalizedProgress;
    //Vector3 _endPos;
    //Vector3 _startPos;
    //float _beatTime;
    //float _resetTime;
    //public override void OnEnter()
    //{
    //    //_normalizedProgress = 0;
    //    //~(1 << context.gameObject.layer)
    //    var hit = Physics2D.Raycast(context.transform.position, -context.transform.up, 999, LayerMask.GetMask("Ground"));
    //    Debug.Log(hit.collider);
    //    _endPos = hit.point + Vector2.up/2/*half sprite height*/;//will spawn at 0 if there is no surface
    //    _startPos = context.transform.position;
    //    _beatTime = 1f / (SongManager.Instance.Song.Bpm / 60f) * context.BeatsForCompletion;
    //    _resetTime = Time.time;
    //}
    //public override void Update()//how can I relate falling to bpm?
    //{
    //    //_normalizedProgress += context.FallAccelerationBuildUpSpeed * Time.deltaTime;
    //    //if (_normalizedProgress > 1) _normalizedProgress = 1;

    //    //var dir = -context.transform.up;
    //    //var buildUp = context.FallCurve.Evaluate(_normalizedProgress);
    //    //var delta = parent != null ? parent.position - _startRelativeToParent : Vector3.zero;

    //    //var pos = context.transform.position + buildUp * context.MaxFallSpeed * Time.deltaTime * dir;
    //    var hit = Physics2D.Raycast(context.transform.position, -context.transform.up, 999, LayerMask.GetMask("Ground"));
    //    _endPos = hit.point + Vector2.up / 2/*half sprite height*/;//will spawn at 0 if there is no surface
    //    var normalizedProgress = Mathf.Clamp((Time.time - _resetTime) / _beatTime, 0, 1);
    //    var easing = context.FallCurve.Evaluate(normalizedProgress);

    //    context.Rigidbody.MovePosition(Vector3.Lerp(_startPos, _endPos, easing));

    //    if (context.Grounded)
    //    {
    //        //var point =  Physics2D.Raycast(context.transform.position, -context.transform.up, 1, 1 << LayerMask.NameToLayer("Ground")).point;
    //        //context.transform.position = point + (Vector2)context.transform.up * 0.5f;
    //        context.TransitionToState(typeof(Idle));
    //    }
    //}
}
