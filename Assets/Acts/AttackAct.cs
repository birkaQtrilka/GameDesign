using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Acts/Attack")]
public class AttackAct : Act
{
    [SerializeField] bool _oppositeWay;
    public override void Perform(MovementStateMachine context)
    {
        if (_oppositeWay)
        {
            context.transform.right *= -1;
            context.GetComponent<IAttacker>().Attack();
            context.transform.right *= -1;

        }
        else
        {
            context.GetComponent<IAttacker>().Attack();

        }
    }
}
