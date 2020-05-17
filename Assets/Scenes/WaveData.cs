using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct WaveData : IComponentData
{
    public float amplitude;
    public float xOffset;
    public float yOffeet;
}
