using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace TD.Components
{
    [GenerateAuthoringComponent]
    public struct StartWaypointTag : IComponentData
    {
        public float timer;
        public float spawnTime;
    }
    
    public struct ReachedEnd : IComponentData
    {
    }
}