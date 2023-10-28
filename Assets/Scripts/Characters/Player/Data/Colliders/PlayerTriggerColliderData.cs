using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerTriggerColliderData
{
    [field: SerializeField] public BoxCollider GroundCheckerCollider { get; private set; }
    public Vector3 GroundCheckExtends { get; private set; }

    public void Initialize()
    {
        GroundCheckExtends = GroundCheckerCollider.bounds.extents;
    }
}
