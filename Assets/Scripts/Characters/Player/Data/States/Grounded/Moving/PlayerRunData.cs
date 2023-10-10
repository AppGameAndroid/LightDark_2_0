using System;
using UnityEngine;

[Serializable]
public class PlayerRunData 
{
    [field: SerializeField][field: Range(1f, 2f)] public float runSpeed = 1f;
}
