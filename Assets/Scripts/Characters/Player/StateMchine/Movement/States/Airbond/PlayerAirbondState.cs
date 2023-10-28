using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirbondState : PlayerMoveState
{
    public PlayerAirbondState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AirborneParameterHash);
        ResetSpringState();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AirborneParameterHash);
    }
    #endregion

    #region Reusable Methods 

    protected override void OnContactWithGround(Collider collider)
    {
        stateMachine.ChangeState(stateMachine.lightLandingState);
    }

    protected virtual void ResetSpringState()
    {
        stateMachine.ReusableData.ShouldSpring = false;
    }
    #endregion
}
