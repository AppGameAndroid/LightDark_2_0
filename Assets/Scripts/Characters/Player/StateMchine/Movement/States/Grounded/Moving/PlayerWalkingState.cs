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
        stateMachine.ReusableData.MovementSpeedModifier = movementData.WalkData.walkSpeed;
    }
    #endregion

    #region InputRegion
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        base.OnWalkToggleStarted(context);
        stateMachine.ChangeState(stateMachine.runningState);
    }

    protected override void OnMomementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.lightStoppingState);
    }
    #endregion
}
