using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerWalkData 
{
    [field:SerializeField][field: Range(0f,1f)] public float SpeedModifier = 0.225f;
}
