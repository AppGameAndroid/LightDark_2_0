using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMidStoppingState : PlayerStoppingState
{
    public PlayerMidStoppingState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementDesacelerationForce = movementData.StopData.MidtDecelerationForce;
        stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.MediumForce;
    }
    #endregion
}
