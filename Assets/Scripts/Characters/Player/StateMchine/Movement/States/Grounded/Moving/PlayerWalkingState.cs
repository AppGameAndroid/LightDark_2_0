using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerMomentState
{
    public PlayerWalkData WalkData;

    public PlayerWalkingState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
        WalkData = movementData.WalkData;
    }

    #region Istate Methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = WalkData.SpeedModifier;

        stateMachine.ReusableData.backwardsCamaraRecenterData = WalkData.backwardsCamaraRecenterData;
        base.Enter();

        stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.WeakForce;
    }
    public override void Exit()
    {
        base.Exit();
        SetBaseCamaraRecenterData();
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

        base.OnMovementCanceled(context);
    }
    #endregion
}
