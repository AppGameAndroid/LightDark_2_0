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
    public PlayerDashState dashState { get; }
    public PlayerRunningState runningState { get; }
    public PlayerSprintState sprintState { get; }
    public PlayerLightStoppingState lightStoppingState { get; }
    public PlayerMidStoppingState midStoppingState { get; }
    public PlayerHardStoppingState hardStoppingState { get; }

    public PlayerJumpState jumpState { get; }

    public PlayerMovementSTM(Player player)
    {
        Player = player;
        ReusableData = new PlayerStateReusableData();
        
        dashState = new PlayerDashState(this);
        
        idleState = new PlayerIdleState(this);

        walkState = new PlayerWalkingState(this);
        runningState = new PlayerRunningState(this);
        sprintState = new PlayerSprintState(this);

        lightStoppingState = new PlayerLightStoppingState(this);
        midStoppingState = new PlayerMidStoppingState(this);
        hardStoppingState = new PlayerHardStoppingState(this);  
       
        jumpState = new PlayerJumpState(this);

    }
}
