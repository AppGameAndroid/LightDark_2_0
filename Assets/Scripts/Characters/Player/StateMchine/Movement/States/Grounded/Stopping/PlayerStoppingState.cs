using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStoppingState : PlayerGroundedState
{
    public PlayerStoppingState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

    }

    public override void PhysicsUpdates()
    {
        base.PhysicsUpdates();
        if (!IsMovingHorizontaly())
        {
            return;
        }
        DecelerationOnHorizontal();
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.idleState);
    }
    #endregion

    #region Reusable Methods

    protected override void AddInputActionCallBacks()
    {
        base.AddInputActionCallBacks();
        stateMachine.Player.Input.PlayerActions.Movement.started += OnmovementStared;
    }

    protected override void RemoveInputActionCallBacks()
    {
        base.RemoveInputActionCallBacks();
        stateMachine.Player.Input.PlayerActions.Movement.started -= OnmovementStared;
    }
    #endregion

    #region Input Methods
    protected override void OnMomementCanceled(InputAction.CallbackContext context)
    {
    }
    private void OnmovementStared(InputAction.CallbackContext context)
    {
        OnMove();
    }
    #endregion
}
