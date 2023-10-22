using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : IState
{
    protected PlayerMovementSTM stateMachine;

    protected PlayerGroundedData movementData;

    protected PlayerAirboneData airboneData;

    #region  IState Method
    public PlayerMoveState(PlayerMovementSTM playerMovementstateMachine)
    {
        stateMachine = playerMovementstateMachine;
        movementData = stateMachine.Player.pData.GroundedData;
        airboneData = stateMachine.Player.pData.airboneData;
        InitializeData();
    }

    public virtual void Enter()
    {
        Debug.Log("State: " + GetType().Name);
        //Para conocer el estado actual del jugador 
        
     

        AddInputActionCallBacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionCallBacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void Update()
    {

    }
    public virtual void PhysicsUpdates()
    {
        Move();
    }

    public virtual void OnAnimationEnterEvent()
    {

    }

    public virtual void OnAnimationExitEvent()
    {

    }

    public virtual void OnAnimationTransitionEvent()
    {

    }
    public virtual void OnTriggerEnter(Collider collider)
    {
        if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
        {
            OnContactWithGround(collider);
            return;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
        {
            OnContactWithGroundExited(collider);
            return;
        }
    }
    
    private void InitializeData()
    {
        SetBaseRotationData();
    }
    #endregion

    #region  Main Method
    private void ReadMovementInput()
    {
        stateMachine.ReusableData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.MovementSpeedModifier == 0f)
        {
            return;
        }

        Vector3 movementDirection = GetMovementInputDirection();

        float targetRotationYAngle = Rotate(movementDirection);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        float movementSpeed = GetMovementSpeed();

        Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

        stateMachine.Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
    }

    private float Rotate(Vector3 direction)
    {
        float directionAngle = UpdateTargetRotation(direction);

        RotateTowardsTargetDirection();
        return directionAngle;

    }

    private void UpdateTargetRotationData(float targetAngle)
    {
        stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;
        stateMachine.ReusableData.DampedTargetRotatioPassedTime.y = 0f;
    }

    private float AddCamaraRotationAngle(float angle)
    {
        angle += stateMachine.Player.MainCamaraTransform.eulerAngles.y;

        if (angle > 360f)
        {
            angle -= 360f;
        }

        return angle;
    }

    private float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        // convierte el las catidad de radianes para conseguir el angulo 

        if (directionAngle < 0f)
        {
            directionAngle += 360f;
            // conseguir solo angulos positivos; 
        }

        return directionAngle;
    }

    #endregion

    #region Reusable Area

    

    protected void SetBaseRotationData()
    {
        stateMachine.ReusableData.RotationData = movementData.BaseRotationData;
        stateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
    }
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(stateMachine.ReusableData.MovementInput.x, 0f, stateMachine.ReusableData.MovementInput.y);
    }

    protected float GetMovementSpeed()
    {
        return movementData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier * stateMachine.ReusableData.MovementOnSlopeSpeedModify;
    }

    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;
        playerHorizontalVelocity.y = 0f;

        return playerHorizontalVelocity;
    }

    protected Vector3 GetPlayerVerticalVelocity()
    {
        return new Vector3(0f, stateMachine.Player.Rigidbody.velocity.y, 0f);
    }
    protected void RotateTowardsTargetDirection()
    {
        float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;

        if (currentYAngle == stateMachine.ReusableData.CurrentTargetRotation.y)
        {
            return;
        }

        float smoothY_Angle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ReusableData.CurrentTargetRotation.y, ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableData.TimeToReachTargetRotation.y - stateMachine.ReusableData.DampedTargetRotatioPassedTime.y);

        stateMachine.ReusableData.DampedTargetRotatioPassedTime.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, smoothY_Angle, 0f);

        stateMachine.Player.Rigidbody.MoveRotation(targetRotation);

    }

    protected float UpdateTargetRotation(Vector3 direction, bool ShouldConsidereCamareRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (ShouldConsidereCamareRotation)
        {
            directionAngle = AddCamaraRotationAngle(directionAngle);
        }


        if (directionAngle != stateMachine.ReusableData.CurrentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }

    protected Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        // normaliza los puntos del angulo 
    }

    protected void ResetVelocity()
    {
        stateMachine.Player.Rigidbody.velocity = Vector3.zero;
        Debug.Log("la velocidad a sido reseteada");
    }

    protected virtual void AddInputActionCallBacks()
    {
        stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
        
    }

    protected virtual void RemoveInputActionCallBacks()
    {
        stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
        
    }

    protected void DesacelerationOnHorizontal()
    {
        Vector3 PlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
        stateMachine.Player.Rigidbody.AddForce(-PlayerHorizontalVelocity * stateMachine.ReusableData.MovementDesacelerationForce, ForceMode.Acceleration);
    }
    
    protected void DesacelerationOnVertical()
    {
        Vector3 PlayerVerticalVelocity = GetPlayerVerticalVelocity();
        stateMachine.Player.Rigidbody.AddForce(-PlayerVerticalVelocity * stateMachine.ReusableData.MovementDesacelerationForce, ForceMode.Acceleration);
    }

    protected bool IsMovingHorizontaly(float minimunMagnitud = 0.1f)
    {
        Vector3 PlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

        Vector2 playerHorizontalMoment = new Vector2(PlayerHorizontalVelocity.x, PlayerHorizontalVelocity.z);
        return playerHorizontalMoment.magnitude > minimunMagnitud;
    }

    protected bool isMovingUp(float minimumMagnitud = 0.1f)
    {
        
        return  GetPlayerVerticalVelocity().y > minimumMagnitud;
    }

    protected bool isMovingDown(float minimumMagnitud = 0.1f)
    {

        return GetPlayerVerticalVelocity().y < -minimumMagnitud;
    }
    protected Vector3 ResetVerticalVelocity()
    {
        Vector3 playerHorizaontalVelocity = GetPlayerHorizontalVelocity();
        stateMachine.Player.Rigidbody.velocity = playerHorizaontalVelocity;
        return playerHorizaontalVelocity;
    }

    #endregion

    #region Input Methods 
    protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        stateMachine.ReusableData.ShouldWalk = !stateMachine.ReusableData.ShouldWalk;
        // cada vez que agregamos un callback nesecitamos eliminarla
        Debug.Log("OnWalkToggle activado PlaYerMoveState ");
    }
    protected virtual void OnContactWithGround(Collider collider)
    {

    }

    protected virtual void OnContactWithGroundExited(Collider collider)
    {

    }
    #endregion
}
