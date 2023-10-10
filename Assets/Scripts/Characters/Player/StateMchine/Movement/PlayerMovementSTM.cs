using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerMovementSTM : StateMachine
{
    public Player Player { get;}

    public PlayerStateReusableData ReusableData { get;}
    public PlayerIdleState idleState { get; }
    public PlayerWalkingState walkState { get; }
    public PlayerRunningState runningState { get; }
    public PlayerSprintState sprintState { get; }

    public PlayerMovementSTM(Player player)
    {
        Player = player;
        ReusableData = new PlayerStateReusableData();
        idleState = new PlayerIdleState(this);
        walkState = new PlayerWalkingState(this);
        runningState = new PlayerRunningState(this);
        sprintState = new PlayerSprintState(this);
    }
}
