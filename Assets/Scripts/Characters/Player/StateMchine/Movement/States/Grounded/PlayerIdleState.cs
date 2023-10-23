using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    private PlayerIdleData idleData;
    public PlayerIdleState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
        idleData = movementData.IdleData;
    }

    #region Istate Methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;
        stateMachine.ReusableData.backwardsCamaraRecenterData = idleData.backwardsCamaraRecenterData;
        base.Enter();
        
        
        stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.StationayForce;
        ResetVelocity(); 
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.ReusableData.MovementInput == Vector2.zero) 
        {
            return;
        }
        OnMove();
    }

    public override void PhysicsUpdates()
    {
        base.PhysicsUpdates();
        if (!IsMovingHorizontaly())
        {
            return;
        }
        ResetVelocity();
    }
    #endregion

}
