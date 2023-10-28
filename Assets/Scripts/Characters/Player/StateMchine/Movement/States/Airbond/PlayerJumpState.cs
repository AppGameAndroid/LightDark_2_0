using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpState : PlayerAirbondState
{
    private bool shouldKeepRotating; 
    private PlayerJumpData jumpData;
    private bool canStartFalling; 
    public PlayerJumpState(PlayerMovementSTM playerMovementstateMachine) : base(playerMovementstateMachine)
    {
        jumpData = airboneData.JumpData;
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();
        
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        stateMachine.ReusableData.MovementDesacelerationForce = jumpData.DesacelerationForce;
        

        stateMachine.ReusableData.RotationData = jumpData.rotationDataJ;
        shouldKeepRotating = stateMachine.ReusableData.MovementInput != Vector2.zero;

        Jump();
    }

    public override void Update()
    {
        base.Update();
        if (!canStartFalling && isMovingUp(0f))
        {
            canStartFalling=true;
        }

        if (!canStartFalling || GetPlayerVerticalVelocity().y > 0 )
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.fallingState);
    }

    public override void PhysicsUpdates()
    {
        base.PhysicsUpdates();

        if (shouldKeepRotating)
        {
            RotateTowardsTargetDirection();
        }
        if (isMovingUp())
        {
            DesacelerationOnVertical();
        }
    }

    public override void Exit()
    {
        
        base.Exit();

        SetBaseRotationData();
        canStartFalling = false;

    }
    #endregion
    #region Main Methods
    private void Jump()
    {
        Vector3 jumpForce = stateMachine.ReusableData.CurrentJumpForce;

        Vector3 jumpDirection = stateMachine.Player.transform.forward;
        // evitar que el salto sea backward . 

        if (shouldKeepRotating)
        {
            UpdateTargetRotation(GetMovementInputDirection());
            
            jumpDirection = GetTargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
        }

        jumpForce.x *= jumpDirection.x;
        jumpForce.z *= jumpDirection.z;

        Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ColliderUtility.capsuleColliderData.Collider.bounds.center;

        Ray downwardRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardRayFromCapsuleCenter, out RaycastHit hit, jumpData.jumpToGroundRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            float groundAngle = Vector3.Angle(hit.normal, -downwardRayFromCapsuleCenter.direction);

            if (isMovingUp())
            {
                float forceModifier = jumpData.jumpForceModifierOnSlopeUpwards.Evaluate(groundAngle);
                jumpForce.x *= forceModifier;
                jumpForce.z *= forceModifier;
            }

            if (isMovingDown())
            {
                float forceModifier = jumpData.jumpForceModifierOnSlopeDownwards.Evaluate(groundAngle);
                jumpForce.y *= forceModifier;

            }
        }
        else
        {
            float groundAngle = Vector3.Angle(hit.normal, -Vector3.down);
        }

        ResetVelocity();
        Debug.Log("la fuerza agregada al salto es de = " + jumpForce);
        stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
    }
    #endregion

    #region Main Methods
    protected override void ResetSpringState()
    {
        
    }
    #endregion

    #region Input Methods
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
    }

    #endregion

}
