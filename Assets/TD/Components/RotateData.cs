using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace TD.Components
{
    [GenerateAuthoringComponent]
    public struct RotateData : IComponentData
    {
        public float speed;
        public float angle;
        public quaternion dir;
    }
}