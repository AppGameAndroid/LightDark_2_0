using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerCamaraUtility
{

    [field: SerializeField] public CinemachineVirtualCamera VirtualCamera { set; private get; }
    [field: SerializeField] public float defaultHorizontalWaitTime { set; private get; } = 0f;
    [field: SerializeField] public float defaultHorizontalRecenterTime { set; private get; } = 4f;
    private CinemachinePOV CinemachinePOV;

    public void Inicialaze()
    {
        CinemachinePOV = VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }
    public void EnableRecenterCamara(float waitTime= -1f, float recenteringTime =-1f, float baseMovementSpeed =1f, float movementSpeed = 1f)
    {
        CinemachinePOV.m_HorizontalRecentering.m_enabled = true;
        CinemachinePOV.m_HorizontalRecentering.CancelRecentering();
        if (waitTime == -1f)
        {
            waitTime = defaultHorizontalWaitTime;
        }

        if (recenteringTime == -1f)
        {
            recenteringTime = defaultHorizontalRecenterTime;
        }

        recenteringTime = recenteringTime * baseMovementSpeed / movementSpeed;
        CinemachinePOV.m_HorizontalRecentering.m_WaitTime = waitTime;
        CinemachinePOV.m_HorizontalRecentering.m_RecenteringTime = recenteringTime;
    }
    public void DisableRecenterCamara() 
    {
        CinemachinePOV.m_HorizontalRecentering.m_enabled = false;
    }

}
