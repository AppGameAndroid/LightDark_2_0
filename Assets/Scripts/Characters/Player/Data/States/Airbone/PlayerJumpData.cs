using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class PlayerJumpData
{
    [field: SerializeField] public PlayerRotationData rotationDataJ { get; private set; }
    [field: SerializeField][field: Range(0f, 5f)] public float jumpToGroundRayDistance { get; private set; } = 3f;
    [field: SerializeField] public AnimationCurve jumpForceModifierOnSlopeUpwards { get; private set; }
    [field: SerializeField] public AnimationCurve jumpForceModifierOnSlopeDownwards { get; private set; }
    
    [field: SerializeField] public Vector3 StationayForce { get; private set; }
    [field: SerializeField] public Vector3 WeakForce {get;  private set; }
    [field: SerializeField] public Vector3 MediumForce {get;  private set; }
    [field: SerializeField] public Vector3 StrongForce {get;  private set; }
    [field: SerializeField][field: Range(0f, 10f)] public float DesacelerationForce { get; private set; } = 0.5f;

}
