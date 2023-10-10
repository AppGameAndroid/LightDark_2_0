using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshrender : MonoBehaviour
{
    private SkinnedMeshRenderer mr;
    void Awake()
    {
        mr = GetComponent<SkinnedMeshRenderer>();
    }

    public void Start()
    {
        Debug.Log(mr.bounds.size);
    }
}
