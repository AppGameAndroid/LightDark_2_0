using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerMovementSTM : StateMachine
{
    public Player Player { get;}

    public PlayerStateReusableData ReusableData { get;}
    public PlayerIdleState idleState { get; }

    //Moving States
    public PlayerWalkingState walkState { get; }
    public PlayerDashState dashState { get; }
    public PlayerRunningState runningState { get; }
    public PlayerSprintState sprintState { get; }
    
    //Stops
    public PlayerLightStoppingState lightStoppingState { get; }
    public PlayerMidStoppingState midStoppingState { get; }
    public PlayerHardStoppingState hardStoppingState { get; }

    //Landing
    public PlayerLightLandingState lightLandingState { get; }
    public PlayerRollingLandingState rollingLandingState { get; }
    public PlayerHardLandingState hardLandingState { get; }
    
    //Airbond
    public PlayerJumpState jumpState { get; }
    public PlayerFallingState fallingState { get; }

public PlayerMovementSTM(Player player)
    {
        Player = player;
        ReusableData = new PlayerStateReusableData();

        idleState = new PlayerIdleState(this);

        //Moving States
        dashState = new PlayerDashState(this);
        walkState = new PlayerWalkingState(this);
        runningState = new PlayerRunningState(this);
        sprintState = new PlayerSprintState(this);

        //Stops
        lightStoppingState = new PlayerLightStoppingState(this);
        midStoppingState = new PlayerMidStoppingState(this);
        hardStoppingState = new PlayerHardStoppingState(this);  
       
        //Airbond
        jumpState = new PlayerJumpState(this);
        fallingState = new PlayerFallingState(this);

        //Landing
        lightLandingState = new PlayerLightLandingState(this);
        rollingLandingState = new PlayerRollingLandingState(this);
        hardLandingState = new PlayerHardLandingState(this);
    }
}
