using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct SpawnNeedComponent : IComponentData
{
    public int x;
    public int y;
}
