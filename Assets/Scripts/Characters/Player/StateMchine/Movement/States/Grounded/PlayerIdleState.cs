using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    #region Istate Methods
    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModify = 0;
        ResetVelocity(); 
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.ReusableData.MovementInput == Vector2.zero) 
        {
            return;
        }
        OnMove();
    }

    #endregion

}
