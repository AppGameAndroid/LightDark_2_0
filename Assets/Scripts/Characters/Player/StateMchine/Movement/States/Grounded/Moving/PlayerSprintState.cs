using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprintState : PlayerMomentState
{
    private PlayerSpringData springData;
    private float starTime;
    private bool keepSprinting;
    
   public PlayerSprintState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
        springData = movementData.SpringData;
    }

    #region IState
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementSpeedModifier = springData.SpeedModifier;
        starTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (keepSprinting) 
        {
            return;
        }
        if (Time.time > starTime + springData.SpeedToRunTime) 
        {
            return;
        }
        StopSpring();
    }

    public override void Exit()
    {
        base.Exit();
        keepSprinting = false;
    }
    #endregion

    #region Main Region 

    private void StopSpring()
    {
        if (stateMachine.ReusableData.MovementInput== Vector2.zero) 
        { 
            stateMachine.ChangeState(stateMachine.idleState); 
            return;
        }
        stateMachine.ChangeState(stateMachine.runningState);
    }
    #endregion

    #region Reusable Methods
    protected override void AddInputActionCallBacks()
    {
        base.AddInputActionCallBacks();
        stateMachine.Player.Input.PlayerActions.Sprint.performed += OnsprintPerformance;
    }

    protected override void RemoveInputActionCallBacks()
    {
        stateMachine.Player.Input.PlayerActions.Sprint.performed -= OnsprintPerformance;
    }
    #endregion


    #region Input Methods
    private void OnsprintPerformance(InputAction.CallbackContext context)
    {
        keepSprinting = true;
    }

    protected override void OnMomementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.hardStoppingState);
    }
    #endregion
}
