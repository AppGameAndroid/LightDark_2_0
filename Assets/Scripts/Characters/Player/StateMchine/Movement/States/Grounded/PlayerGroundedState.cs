using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMoveState
{
    public PlayerGroundedState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    #region Reusable Methods 

    protected override void AddInputActionCallBacks()
    {
        base.AddInputActionCallBacks();
        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMomementCanceled;
    }

    protected override void RemoveInputActionCallBacks()
    {
        base.RemoveInputActionCallBacks();
        stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMomementCanceled;
    }


    protected virtual void OnMove()
    {
        if (stateMachine.ReusableData.ShouldWalk)
        {
            stateMachine.ChangeState(stateMachine.walkState);
            return;
        }
        stateMachine.ChangeState(stateMachine.runningState);
    }
    #endregion

    #region InputRegion

    protected virtual void OnMomementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.idleState);
    }
    #endregion
}
