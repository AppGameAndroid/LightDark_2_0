using System;
using UnityEngine;

[Serializable]
public class PlayerCamaraRecenterData
{
    [field: SerializeField][field: Range(0f,360f)] public float minAngle {get; private set;}
    [field: SerializeField][field: Range(0f,360f)] public float maxAngle {get; private set;}
    [field: SerializeField][field: Range(-1f,20f)] public float waitTime {get; private set;}
    [field: SerializeField][field: Range(-1f,20f)] public float recenterTime {get; private set;}

    public bool IsWithRange(float angle)
    {
        return angle >= minAngle && angle <= maxAngle;  
    }
}
