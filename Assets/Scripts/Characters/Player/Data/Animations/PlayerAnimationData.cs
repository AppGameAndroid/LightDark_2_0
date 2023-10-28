using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [Header("Grupo de Estados Nombres Parametros ")]

    [SerializeField] private string groundedParameterName = "Grounded";
    [SerializeField] private string movingParameterName = "Moving";
    [SerializeField] private string airborneParameterName = "Airborne";
    [SerializeField] private string stopingParameterName = "Stoping";
    [SerializeField] private string landingParameterName = "Landing";

    [Header("Grounded Parametros ")]

    [SerializeField] private string idleParameterName = "isIdling";
    [SerializeField] private string dashParameterName = "isDashing";
    [SerializeField] private string walkParameterName = "isWalking";
    [SerializeField] private string runParameterName = "isRunning";
    [SerializeField] private string sprintParameterName = "isSprinting";
    [SerializeField] private string MidStopParameterName = "isMediumStop";
    [SerializeField] private string HardStopParameterName = "isHardStop";
    [SerializeField] private string isRollParameterName = "Roll";
    [SerializeField] private string isHardlandingParameterName = "isHardlanding";
    

    [Header("Airbone Parametros ")]

    [SerializeField] private string isFallParameterName = "isFalling";

    public int GroundParameterHash { get; private set; }
    public int MovingParameterHash { get; private set; }
    public int AirborneParameterHash { get; private set; }
    public int StopingParameterHash { get; private set; }
    public int LandingParameterHash { get; private set; }

    public int IdleParameterHash { get; private set; }
    public int DashParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int SprintParameterHash { get; private set; }
    public int MidStopParameterHash { get; private set; }
    public int HardStopParameterHash { get; private set; }
    public int RollParameterHash { get; private set; }
    public int HardLandingParameterHash { get; private set; }

    public int FallParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundedParameterName);
        MovingParameterHash = Animator.StringToHash(movingParameterName);
        AirborneParameterHash = Animator.StringToHash(airborneParameterName);
        StopingParameterHash = Animator.StringToHash(stopingParameterName);
        LandingParameterHash = Animator.StringToHash(landingParameterName);

        IdleParameterHash = Animator.StringToHash(idleParameterName);
        DashParameterHash = Animator.StringToHash(dashParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        SprintParameterHash = Animator.StringToHash(sprintParameterName);
        MidStopParameterHash = Animator.StringToHash(MidStopParameterName);
        HardStopParameterHash = Animator.StringToHash(HardStopParameterName);
        RollParameterHash = Animator.StringToHash(isRollParameterName);
        HardLandingParameterHash = Animator.StringToHash(isHardlandingParameterName);

        FallParameterHash = Animator.StringToHash(isFallParameterName);

    }
}

