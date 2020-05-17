using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct FallComponent : IComponentData
{
    public float3 position;
    public float speed;
}
