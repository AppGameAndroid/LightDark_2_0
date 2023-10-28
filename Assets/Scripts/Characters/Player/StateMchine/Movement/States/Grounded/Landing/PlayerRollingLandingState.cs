using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRollingLandingState : PlayerLandingState
{
    private PlayerRollingData rollingData;
    public PlayerRollingLandingState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
        rollingData = movementData.rollingData;
    }
    #region IState Methods 
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = rollingData.speedModifier;

        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.RollParameterHash);
        stateMachine.ReusableData.ShouldSpring = false;
    }

    public override void PhysicsUpdates()
    {
        base.PhysicsUpdates();

        if (stateMachine.ReusableData.MovementInput != Vector2.zero)
        {
            return;
        }

        RotateTowardsTargetDirection();
    }

    public override void OnAnimationTransitionEvent()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.midStoppingState); 
            return;
        }
        OnMove();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RollParameterHash);
    }
    #endregion

    #region Input Methods 
    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
    }
    #endregion

}
