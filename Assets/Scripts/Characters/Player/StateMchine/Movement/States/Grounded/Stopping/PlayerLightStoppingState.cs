using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightStoppingState : PlayerStoppingState
{
    public PlayerLightStoppingState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementDesacelerationForce = movementData.StopData.LightDecelerationForce;
        stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.WeakForce;
    }
    #endregion
}
