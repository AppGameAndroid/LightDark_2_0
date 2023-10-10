using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CamaraZoom : MonoBehaviour
{
    [SerializeField][Range(0f,10f)] private float defaultDistance = 6f; 
    [SerializeField][Range(0f, 10f)] private float minDistance = 6f; 
    [SerializeField][Range(0f, 10f)]private float maxDistance = 6f;
    
    [SerializeField][Range(0f, 10f)] private float smothing = 4f; 
    [SerializeField][Range(0f, 10f)] private float zoomSensivity =1f;

    private CinemachineFramingTransposer framingTransposer;
    private CinemachineInputProvider inputProvider;

    private float currentTargetDistance;  

    private void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider= GetComponent<CinemachineInputProvider>();
    }

    private void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSensivity;

        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minDistance, maxDistance);
        float currentDistance = framingTransposer.m_CameraDistance;

        if (currentDistance == currentTargetDistance)
        {
            return;
        }

        float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smothing * Time.deltaTime);

        framingTransposer.m_CameraDistance = lerpedZoomValue;

    }
}
