using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHardLandingState : PlayerLandingState
{
    public PlayerHardLandingState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    #region IState Methods 
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.HardLandingParameterHash);
        stateMachine.Player.Input.PlayerActions.Movement.Disable();
        
        ResetVelocity();
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.HardLandingParameterHash);
        stateMachine.Player.Input.PlayerActions.Movement.Enable();
    }
    #endregion

    #region Reusable methods 
    protected override void AddInputActionCallBacks()
    {
        base.AddInputActionCallBacks();
        stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
    }
    protected override void RemoveInputActionCallBacks()
    {
        base.RemoveInputActionCallBacks();
        stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
    }

    protected override void OnMove()
    {
        if (stateMachine.ReusableData.ShouldWalk)
        {
            return;
        }
        stateMachine.ChangeState(stateMachine.runningState);
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
    #endregion

    #region Input Methods
    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
    }
    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        OnMove();
    }
    #endregion
}
