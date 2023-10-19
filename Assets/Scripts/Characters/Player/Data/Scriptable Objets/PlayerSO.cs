using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="player", menuName ="Custom/Character/Player") ]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundedData GroundedData { get; private set; }
    [field: SerializeField] public PlayerAirboneData airboneData { get; private set; }
    // remember change on Unity 
}
