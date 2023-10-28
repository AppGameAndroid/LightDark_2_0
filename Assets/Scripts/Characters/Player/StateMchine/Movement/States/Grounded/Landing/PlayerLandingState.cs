using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }
    #region IState methods 
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.LandingParameterHash);
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.LandingParameterHash);
    }
    #endregion
}
