using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class CapsuleColliderUtility
{
    [field: SerializeField] public CapsuleColliderData capsuleColliderData { get; private set; }
    [field: SerializeField] public DefaultColliderData defaultColliderData { get; private set; }
    [field: SerializeField] public SlopeData  slopeData { get; private set; }
 
    public void Initialize (GameObject gameObject)
    {
        if (capsuleColliderData != null)
        {
            return; 
        }
        capsuleColliderData = new CapsuleColliderData();
        capsuleColliderData.Initialize (gameObject);

    }

    protected virtual void OnInitialize()
    {

    }
    public void CalculateCapsuleColliderDimentions()
    {
        SetCapsuleColliderRadius(defaultColliderData.Radius);
        SetCapsuleColliderHeigth(defaultColliderData.Height * (1f - slopeData.StepHeightPercentage));
        RecalculateCapsulecolliderCenter();

        float halfColliderHeight = capsuleColliderData.Collider.height / 2f;

        if (halfColliderHeight < capsuleColliderData.Collider.radius ) 
        {
            SetCapsuleColliderRadius(halfColliderHeight);
        }

        capsuleColliderData.UpdateColliderData();
    }

    public void SetCapsuleColliderRadius(float radius)
    {
        capsuleColliderData.Collider.radius = radius;
    }

    public void SetCapsuleColliderHeigth( float height)
    {
        capsuleColliderData.Collider.height = height;
    }

    public void RecalculateCapsulecolliderCenter()
    {
        float colliderHeightDifference = defaultColliderData.Height - capsuleColliderData.Collider.height;
        Vector3 newColliderCenter = new Vector3(0f, defaultColliderData.CenterY + (colliderHeightDifference / 2f), 0f);

        capsuleColliderData.Collider.center = newColliderCenter;
    }
}
