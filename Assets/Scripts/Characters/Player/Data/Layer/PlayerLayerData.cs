using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerLayerData 
{
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }

    public bool ContainerLayer(LayerMask layerMask, int layer)
    {
        // Layer son 32 representados en binario
        return (1 << layer & layerMask) != 0;
        // << es llamado Bitwise Shifting es decir agrega el numero de 0 a la derecha 
    }

    public bool IsGroundLayer(int layer)
    {
        return ContainerLayer(GroundLayer, layer);  
    }

}
