using UnityEngine;

public class Idle : MovementState
{

    public Idle(MovementStateMachine context) : base(context) { }
    bool wasPressed;
    bool wasPressedJump;
    float oldX;
    public override void Update()
    {
        //if(context is Enemy)
        //Debug.Log(context.DirectionInput.x + " " + !wasPressed);
        if (context.DirectionInput.x != 0 && !wasPressed)
        {
            context.TransitionToState(typeof(Walk));
            context.Break = false;
            wasPressed = true;
        }
        else if (context.DirectionInput.x != oldX || context.DirectionInput.x == 0)
            wasPressed = false;

        
        if (context.DirectionInput.y > 0 && context.Grounded && !wasPressedJump)
        {
            context.TransitionToState(typeof(Jump));
            context.Break = false;
            wasPressedJump = true;
        }else if(context.DirectionInput.y < 1)
            wasPressedJump=false;
        if (!context.Grounded)
        {
            context.Break = false;

            context.TransitionToState(typeof(Fall));
        }
        oldX = context.DirectionInput.x;
    }
}
