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
        StartAnimation(stateMachine.Player.AnimationData.HardStopParameterHash);
        stateMachine.ReusableData.MovementDesacelerationForce = movementData.StopData.HardtDecelerationForce;

        stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.StrongForce;
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
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.HardStopParameterHash);
    }
    #endregion
}
