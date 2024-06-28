using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementStateMachine
{

    private void Awake()
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
    void FixedUpdate()
    {
        if (_transitioning) return;
        DirectionInput = new
        ( 
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical")
        );
        CurrentState.Update();

        Rigidbody.velocity = Vector3.zero;
    }


}




