using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Acts/Move")]
public class MoveAct : Act
{
    [SerializeField] Direction _direction;
    public override void Perform(MovementStateMachine context)
    {
        //var hit = Physics2D.Raycast(context.transform.position, context.transform.right, context.WalkStep, ~(1 << context.gameObject.layer) & ~LayerMask.GetMask("GroundOnly"));

        //if (hit.collider != null && !hit.collider.isTrigger) return;

        context.SetDirection(_direction.Value);
        context.StartCoroutine(ZeroAfterFrame(context));

    }
    IEnumerator ZeroAfterFrame(MovementStateMachine context)
    {
        yield return new WaitForFixedUpdate();
        context.SetDirection(Vector2.zero);

    }
}