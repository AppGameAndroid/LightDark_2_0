using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMomentState : PlayerGroundedState
{
    public PlayerMomentState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    #region IState methods 
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.MovingParameterHash);
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.MovingParameterHash);
    }
    #endregion
}
