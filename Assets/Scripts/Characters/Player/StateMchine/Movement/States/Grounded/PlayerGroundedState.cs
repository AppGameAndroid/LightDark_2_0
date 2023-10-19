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
        UpdateShouldspringState();
        
    }

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
            float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

            float slopeSpeedModifier = SetSloopSpeepModifierOnAnlge(groundAngle);
            if (slopeSpeedModifier == 0f)
            {
                return;
            }
            
            float distanceToFloatingPoint = stateMachine.Player.ColliderUtility.capsuleColliderData.ColliderCenterInLocalSpace.y *stateMachine.Player.transform.localScale.y - hit.distance;
            if (distanceToFloatingPoint == 0f) 
            {
                return;
            }

            float amountToLift = distanceToFloatingPoint * slopeData.StepReachForce - GetPlayerVerticalVelocity().y;

            // Registros de depuraci�n
            

            Vector3 liftForce = new Vector3 (0f, amountToLift, 0f);
            stateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            // Registro de depuraci�n despu�s de aplicar la fuerza
            
        }
    }

    private float SetSloopSpeepModifierOnAnlge(float Angle)
    {
        float slopeSpeedModifier = movementData.SloopeSpeedAngle.Evaluate (Angle);
        stateMachine.ReusableData.MovementOnSlopeSpeedModify = slopeSpeedModifier;
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

        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
        stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStarted;
        stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
    }

  

    protected override void RemoveInputActionCallBacks()
    {
        base.RemoveInputActionCallBacks();
        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
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

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context) 
    { 
    
    }

    #endregion
}
