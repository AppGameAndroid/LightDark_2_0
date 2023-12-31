using System.Collections.Generic;
using UnityEngine;

public class PlayerStateReusableData 
{
    public Vector2 MovementInput { get; set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float MovementOnSlopeSpeedModify { get; set; } = 1f;
    public float MovementDesacelerationForce { get; set; } = 1f;
    public List<PlayerCamaraRecenterData> backwardsCamaraRecenterData { get;  set; }
    public List<PlayerCamaraRecenterData> sideWaysCamaraRecenterData { get;  set; }
    

    public bool ShouldWalk { get; set; }
    public bool ShouldSpring { get; set; }

    private Vector3 currentTargetRotation;
    private Vector3 timeToReachTargetRotation;
    private Vector3 dampedTargetRotationCurrentVelocity;
    private Vector3 dampedTargetRotatioPassedTime;
    // vector 3 no acepta cambios fuera de la clase 

    public ref Vector3 CurrentTargetRotation
    {
        get
        {
            return ref currentTargetRotation;
        }
    } 
    public ref Vector3 TimeToReachTargetRotation
    {
        get
        {
            return ref timeToReachTargetRotation;
        }
    } 
    
    public ref Vector3 DampedTargetRotationCurrentVelocity
    {
        get
        {
            return ref dampedTargetRotationCurrentVelocity;
        }
    } 
    public ref Vector3 DampedTargetRotatioPassedTime
    {
        get
        {
            return ref dampedTargetRotatioPassedTime;
        }
    }

    public Vector3 CurrentJumpForce { get; set; } 

    public PlayerRotationData RotationData { get; set; }
}
