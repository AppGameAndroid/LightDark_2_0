using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMoveState
{
    private SlopeData slopeData;
    public PlayerGroundedState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
        slopeData = stateMachine.Player.ColliderUtility.slopeData;
    }

    #region IState Methods
    public override void PhysicsUpdates()
    {
        base.PhysicsUpdates();

        Float();
    }
    #endregion

    #region Main Methods 
    private void Float()
    {
        Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ColliderUtility.capsuleColliderData.Collider.bounds.center;
        Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, slopeData.FloatRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            float groundAngle = Vector3.Angle(hit.normal, - downwardsRayFromCapsuleCenter.direction);

            float slopeSpeedModifier = SetSloopSpeepModifierOnAnlge(groundAngle);
            if (slopeSpeedModifier == 0f)
            {
                return;
            }
            
            float distanceToFloatingPoint = stateMachine.Player.ColliderUtility.capsuleColliderData.ColliderCenterInLocalSpace.y * stateMachine.Player.transform.localScale.y - hit.distance;
            if (distanceToFloatingPoint == 0f) 
            {
                return;
            }

            float amountToLift = distanceToFloatingPoint * slopeData.StepReachForce - GetPlayerVerticalVelocity().y;
            Vector3 liftForce = new Vector3 (0f, amountToLift, 0f);
            stateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);

        }
    }

    private float SetSloopSpeepModifierOnAnlge(float Angle)
    {
        float slopeSpeedModifier = movementData.SloopeSpeedAngle.Evaluate (Angle);
        stateMachine.ReusableData.MovementOnSlopeSpeedModify = slopeSpeedModifier;
        return slopeSpeedModifier;
    }
    #endregion

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
