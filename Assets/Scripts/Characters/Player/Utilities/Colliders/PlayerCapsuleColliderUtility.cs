using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerCapsuleColliderUtility :CapsuleColliderUtility
{
    [field: SerializeField] public PlayerTriggerColliderData TriggerData { get; private set; }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        TriggerData.Initialize();
    }
}
