using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace TD.Components
{
    public class RotateSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
        }

        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;
            Entities.ForEach((ref Rotation rot, ref RotateData rotateData) =>
            {
                var newRotation = quaternion.identity;
                rotateData.angle += rotateData.speed * dt;
                rot.Value = quaternion.EulerXYZ(math.PI / 2, rotateData.angle, 0);
            }).Run();
        }
    }
}