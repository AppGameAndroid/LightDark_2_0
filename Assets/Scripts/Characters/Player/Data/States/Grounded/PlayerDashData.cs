using System;
using UnityEngine;

[Serializable]
public class PlayerDashData 
{
    [field: SerializeField][field: Range(1f, 3f)] public float SpeeedModifier { get; private set; } = 2f;
    [field: SerializeField]public PlayerRotationData RotationData { get; private set; }
    [field: SerializeField][field: Range(0f, 2f)] public float TimeToBeConsideredConsecitive { get; private set; } = 1f;
    [field: SerializeField][field: Range(1f,10f)] public int ConsecutiveDashesLimitAmount { get; private set; } = 3;
    [field: SerializeField][field: Range(0f, 5f)] public float DashLimitCooldown { get; private set; } = 1.75f;
}
