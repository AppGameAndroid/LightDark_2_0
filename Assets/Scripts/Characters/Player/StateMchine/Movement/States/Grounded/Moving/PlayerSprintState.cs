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
    private bool shouldResetSpring;
    
   public PlayerSprintState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
        springData = movementData.SpringData;
    }

    #region IState
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = springData.SpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.SprintParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.StrongForce;

        starTime = Time.time;

    }

    public override void Update()
    {
        base.Update();

        if (keepSprinting) 
        {
            return;
        }
        if (Time.time < starTime + springData.SpeedToRunTime) 
        {
            return;
        }
        StopSpring();
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("saliendo de sprintar");
        StopAnimation(stateMachine.Player.AnimationData.SprintParameterHash);

        keepSprinting = false;
        if (shouldResetSpring)
        {
            keepSprinting =true;
            stateMachine.ReusableData.ShouldSpring = false; 
        }

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
        base.RemoveInputActionCallBacks();
        stateMachine.Player.Input.PlayerActions.Sprint.performed -= OnsprintPerformance;
     
    }

    protected override void OnFall()
    {
        shouldResetSpring = false;

        base.OnFall();
    }
    #endregion


    #region Input Methods
    private void OnsprintPerformance(InputAction.CallbackContext context)
    {
        keepSprinting = true;
        stateMachine.ReusableData.ShouldSpring = true;
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("cancelSprint");
        stateMachine.ChangeState(stateMachine.hardStoppingState);
        base.OnMovementCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        shouldResetSpring = false;
        base.OnJumpStarted(context);
    }
    #endregion
}
