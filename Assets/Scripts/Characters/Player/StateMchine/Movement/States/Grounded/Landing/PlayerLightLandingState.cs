using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightLandingState : PlayerLandingState
{
    public PlayerLightLandingState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.StationayForce;

        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();

        if(stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            return;
        }
        OnMove();
    }
    public override void PhysicsUpdates()
    {
        //arreglo para evitar que fisicas hagan caer de golpe 
        base.PhysicsUpdates();
        if (!IsMovingHorizontaly())
        {
            return;
        }
        ResetVelocity();
    }
    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.idleState);
    }

}
