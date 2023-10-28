using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerMomentState
{
    public float startTime;
    private PlayerSpringData springData;
    public PlayerRunningState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
        springData = movementData.SpringData;
    }

    #region Istate
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
        base.Enter();
        
        
        stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.MediumForce;
        startTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (!stateMachine.ReusableData.ShouldWalk)
        {
            return;
        }
        if (Time.time < startTime+springData.RunToWalkTime)
        {
            return;
        }
        StopRunning();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }
    #endregion

    #region Main Methods
    private void StopRunning()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
        stateMachine.ChangeState(stateMachine.walkState);
    }
    #endregion

    #region InputRegion
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        base.OnWalkToggleStarted(context);
        stateMachine.ChangeState(stateMachine.walkState);
    }
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.midStoppingState);

        base.OnMovementCanceled(context);
    }
    #endregion

}
