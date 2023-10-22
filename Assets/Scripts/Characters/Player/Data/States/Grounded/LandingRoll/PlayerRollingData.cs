using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerRollingData : MonoBehaviour
{
    [field: SerializeField][field: Range(0f, 3f)] public float speedModifier { get; set; } = 1f; 
}
