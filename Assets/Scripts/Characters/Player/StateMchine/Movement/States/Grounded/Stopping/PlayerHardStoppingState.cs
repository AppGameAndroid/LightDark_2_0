using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHardStoppingState : PlayerStoppingState
{
    public PlayerHardStoppingState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovemntDesacelerationForce = movementData.StopData.HardtDecelerationForce;
    }
    #endregion
    #region Reusable Methods

    protected override void OnMove()
    {
        if (stateMachine.ReusableData.ShouldWalk)
        {
            return;
        }
        stateMachine.ChangeState(stateMachine.runningState);
    }
    #endregion
}
