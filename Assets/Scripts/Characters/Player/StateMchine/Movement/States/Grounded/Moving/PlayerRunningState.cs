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
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.runSpeed;
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
    #endregion

}
