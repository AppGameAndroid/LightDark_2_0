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
        stateMachine.ReusableData.MovemntDesacelerationForce = movementData.StopData.LightDecelerationForce;
    }
    #endregion
}
