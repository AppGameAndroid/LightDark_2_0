using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashState : PlayerGroundedState
{
    private PlayerDashData dashData;
    private float startTime;
    private int consecutiveDashesUsed;
    private bool ShouldKeepRotatin;

    public PlayerDashState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
        dashData = movementData.DashData;
    }

    #region IState Methods 
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = movementData.DashData.SpeeedModifier;

        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.DashParameterHash);
        stateMachine.ReusableData.RotationData = dashData.RotationData;
        stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.StrongForce;
        
        Dash();

        ShouldKeepRotatin = stateMachine.ReusableData.MovementInput != Vector2.zero;
        
        UpdateConsecutiveDashes();
        
        startTime = Time.time;
    }


    public override void PhysicsUpdates()
    {
        base.PhysicsUpdates();
        if (ShouldKeepRotatin) 
        {
            return;
        }
        RotateTowardsTargetDirection();
    }

    public override void OnAnimationTransitionEvent()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.hardStoppingState);
            return;
        }
        stateMachine.ChangeState(stateMachine.sprintState);
    }

    public override void Exit()
    {
        base.Exit();
        SetBaseRotationData();
        StopAnimation(stateMachine.Player.AnimationData.DashParameterHash);
    }
    #endregion

    #region Main Metohds
    private void UpdateConsecutiveDashes()
    {
        if (IsConsecutive())
        {
            consecutiveDashesUsed = 0;
        }
        ++consecutiveDashesUsed;

        if (consecutiveDashesUsed == dashData.ConsecutiveDashesLimitAmount) 
        {
            consecutiveDashesUsed = 0; 
            stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash, dashData.DashLimitCooldown);
        }
    }

    private bool IsConsecutive()
    {
        return Time.time < startTime + dashData.TimeToBeConsideredConsecitive;
    }

    private void Dash()
    {
        Vector3 DashDirection = stateMachine.Player.transform.forward;

        DashDirection.y = 0f;

        UpdateTargetRotation(DashDirection, false);

        if (stateMachine.ReusableData.MovementInput != Vector2.zero)
        {
            UpdateTargetRotation(GetMovementInputDirection());

            DashDirection = GetTargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
        }
        
        stateMachine.Player.Rigidbody.velocity = DashDirection * GetMovementSpeed(false);
    }
    #endregion

    #region Reusable Methods 
     protected override void AddInputActionCallBacks()
    {
        base.AddInputActionCallBacks();
        stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformanceRotation;
    }

    protected override void RemoveInputActionCallBacks()
    {
        base.RemoveInputActionCallBacks();
        stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformanceRotation;
    }
    #endregion

    #region Input Region 
    private void OnMovementPerformanceRotation(InputAction.CallbackContext context)
    {
        ShouldKeepRotatin = true; 
    }

    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
        
    }
    #endregion
}
