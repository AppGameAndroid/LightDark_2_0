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
        StartAnimation(stateMachine.Player.AnimationData.MidStopParameterHash);
        stateMachine.ReusableData.MovementDesacelerationForce = movementData.StopData.MidtDecelerationForce;
        stateMachine.ReusableData.CurrentJumpForce = airboneData.JumpData.MediumForce;
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.MidStopParameterHash);
    }
    #endregion
}
