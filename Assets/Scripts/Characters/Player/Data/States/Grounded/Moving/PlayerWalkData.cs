using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerWalkData 
{
    [field:SerializeField][field: Range(0f,1f)] public float SpeedModifier = 0.225f;
    [field: SerializeField] public List<PlayerCamaraRecenterData> backwardsCamaraRecenterData { get; private set; }
}
