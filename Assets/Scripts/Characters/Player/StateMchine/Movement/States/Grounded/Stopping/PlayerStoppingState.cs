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
        stateMachine.ReusableData.MovementSpeedModifier = 0f;
        SetBaseCamaraRecenterData();
        base.Enter();        
    }

    public override void PhysicsUpdates()
    {
        base.PhysicsUpdates();

        RotateTowardsTargetDirection();

        if (!IsMovingHorizontaly())
        {
            return;
        }
        DesacelerationOnHorizontal();
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
    private void OnmovementStared(InputAction.CallbackContext context)
    {
        OnMove();
    }
    #endregion
}
