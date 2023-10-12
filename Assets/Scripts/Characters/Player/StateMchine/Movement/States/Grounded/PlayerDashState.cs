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

    public PlayerDashState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
        dashData = movementData.dashData;
    }

    #region IState Methods 
    public override void Enter()
    {
        base.Enter();
        
        stateMachine.ReusableData.MovementSpeedModifier = movementData.dashData.SpeeedModifier;
        
        AddForceOnTransitionaryState();
        UpdateConsecutiveDashes();
        
        startTime = Time.time;
    }

    public override void OnAnimationTransitionEvent()
    {
        base.OnAnimationTransitionEvent();
        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.idleState);
            return;
        }
        stateMachine.ChangeState(stateMachine.sprintState);
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

    private void AddForceOnTransitionaryState()
    {
        if (stateMachine.ReusableData.MovementInput != Vector2.zero)
        {
            return;
        }
        Vector3 characterRotationDirection = stateMachine.Player.transform.forward;

        characterRotationDirection.y = 0f;
        stateMachine.Player.Rigidbody.velocity = characterRotationDirection * GetMovementSpeed();
    }
    #endregion

    #region Input Region 

    protected override void OnMomementCanceled(InputAction.CallbackContext context)
    {
        
    }
    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
       
    }
    #endregion
}
