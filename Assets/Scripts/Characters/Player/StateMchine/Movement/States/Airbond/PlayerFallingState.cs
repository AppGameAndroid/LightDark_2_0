using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirbondState

{
    private PlayerFallData fallData;
    private Vector3 playerPositionEnter; 
    public PlayerFallingState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
        fallData = airboneData.FallData;
    }

    #region IState Mehtods 
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);
        playerPositionEnter = stateMachine.Player.transform.position;
        stateMachine.ReusableData.MovementSpeedModifier = 0f;
        ResetVerticalVelocity();
    }

    public override void PhysicsUpdates()
    {
        base.PhysicsUpdates();
        LimitVerticalVelocity();
    }
    #endregion

    #region Reusable Methods 
    protected override void ResetSpringState()
    {
        // keep springting 
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.FallParameterHash);
    }

    protected override void OnContactWithGround(Collider collider)
    {
        float fallDistance = playerPositionEnter.y - stateMachine.Player.transform.position.y;
        if (fallDistance < fallData.minDistanceHardFall)
        {
            stateMachine.ChangeState(stateMachine.lightLandingState);
            return;
        } 
        if (stateMachine.ReusableData.ShouldWalk &&  !stateMachine.ReusableData.ShouldSpring ||stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.hardLandingState);
            return;
        }
        stateMachine.ChangeState(stateMachine.rollingLandingState);

    }
    #endregion

    #region Main Methods
    private void LimitVerticalVelocity()
    {
        Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

        if (stateMachine.Player.Rigidbody.velocity.y >= -fallData.fallSpeedLimit) 
        {
            return;
        }

        Vector3 limiteVelocity = new Vector3 ( 0f, -fallData.fallSpeedLimit - stateMachine.Player.Rigidbody.velocity.y, 0f );

        stateMachine.Player.Rigidbody.AddForce ( limiteVelocity, ForceMode.VelocityChange );
    }
    #endregion


}
