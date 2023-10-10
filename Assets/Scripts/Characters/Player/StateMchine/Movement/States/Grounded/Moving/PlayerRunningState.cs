using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerMomentState
{
    public PlayerRunningState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
    }

    #region Istate
    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModify = movementData.RunData.runSpeed;
    }
    #endregion

    #region InputRegion
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        base.OnWalkToggleStarted(context);
        stateMachine.ChangeState(stateMachine.walkState);
    }
    #endregion

}
