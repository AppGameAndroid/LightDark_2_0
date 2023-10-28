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

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
        UpdateShouldspringState();
        UpdateCamaraRecenteringState(stateMachine.ReusableData.MovementInput);

    }

    public override void PhysicsUpdates()
    {
        base.PhysicsUpdates();

        Float();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }
    #endregion

    #region Main Methods 
    private void Float()
    {
        Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ColliderUtility.capsuleColliderData.Collider.bounds.center;
        Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, slopeData.FloatRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

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

            // Registros de depuraci�n


            Vector3 liftForce = new Vector3(0f, amountToLift, 0f);
            stateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            // Registro de depuraci�n despu�s de aplicar la fuerza

        }
    }

    private bool isGroundUnderneath()
    {
        BoxCollider groundCheckCollider = stateMachine.Player.ColliderUtility.TriggerData.GroundCheckerCollider;

        Vector3 groundColliderCenterInWorldSpace = groundCheckCollider.bounds.center;

        Collider[] overlapGroundCollider = Physics.OverlapBox(groundColliderCenterInWorldSpace, stateMachine.Player.ColliderUtility.TriggerData.GroundCheckExtends, groundCheckCollider.transform.rotation, stateMachine.Player.LayerData.GroundLayer,QueryTriggerInteraction.Ignore);

        return overlapGroundCollider.Length > 0;
    }

    private float SetSloopSpeepModifierOnAnlge(float Angle)
    {
        float slopeSpeedModifier = movementData.SloopeSpeedAngle.Evaluate(Angle);

        if (stateMachine.ReusableData.MovementOnSlopeSpeedModify != slopeSpeedModifier)
        {
            stateMachine.ReusableData.MovementOnSlopeSpeedModify = slopeSpeedModifier;
            
            UpdateCamaraRecenteringState(stateMachine.ReusableData.MovementInput);
        }
        

        return slopeSpeedModifier;
    }

    private void UpdateShouldspringState()
    {
        if (!stateMachine.ReusableData.ShouldSpring)
        {
            return;
        }
        if (stateMachine.ReusableData.MovementInput != Vector2.zero)
        {
            return;
        }
        stateMachine.ReusableData.ShouldSpring = false;
    }
    #endregion

    #region Reusable Methods 
    protected override void AddInputActionCallBacks()
    {
        base.AddInputActionCallBacks();

        
        stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStarted;
        stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
    }

    protected override void RemoveInputActionCallBacks()
    {
        base.RemoveInputActionCallBacks();
        
        stateMachine.Player.Input.PlayerActions.Dash.started -= OnDashStarted;
        stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;


    }

    protected virtual void OnMove()
    {
        if (stateMachine.ReusableData.ShouldSpring)
        {
            stateMachine.ChangeState(stateMachine.sprintState);
        }
        if (stateMachine.ReusableData.ShouldWalk)
        {
            stateMachine.ChangeState(stateMachine.walkState);
            return;
        }
        stateMachine.ChangeState(stateMachine.runningState);
    }
    #endregion

    #region InputRegion
    protected virtual void OnDashStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.dashState);
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.jumpState);
        Debug.Log("Velocidad en Y antes de la fuerza de elevaci�n: " + stateMachine.Player.Rigidbody.velocity.y);
    }

  

    protected override void OnContactWithGroundExited(Collider collider)
    {
        base.OnContactWithGroundExited(collider);

        if (isGroundUnderneath())
        {
            return;
        }
        Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ColliderUtility.capsuleColliderData.Collider.bounds.center;
        
        Ray downWardsRayFromCapsuleBottom = new Ray(capsuleColliderCenterInWorldSpace - stateMachine.Player.ColliderUtility.capsuleColliderData.colliderVerticalExtents, Vector3.down);

        if (!Physics.Raycast(downWardsRayFromCapsuleBottom,out _, movementData.GroundToFallRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            OnFall();
        }
        
    }

    protected virtual void OnFall()
    {
        stateMachine.ChangeState(stateMachine.fallingState);
    }
    #endregion
}
