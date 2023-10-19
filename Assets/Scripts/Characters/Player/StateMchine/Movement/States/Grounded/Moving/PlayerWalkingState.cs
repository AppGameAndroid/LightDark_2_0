using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerMomentState
{
    public PlayerWalkingState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    #region Istate Methods
    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModifier = movementData.WalkData.SpeedModifier;
        stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.WeakForce;
    }
    #endregion

    #region InputRegion
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        base.OnWalkToggleStarted(context);
        stateMachine.ChangeState(stateMachine.runningState);
    }
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.lightStoppingState);
    }
    #endregion
}
