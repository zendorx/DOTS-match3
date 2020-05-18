using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace TD.Components
{
    [AlwaysSynchronizeSystem]
    public class MoveSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
        }

        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;
            var translations = GetComponentDataFromEntity<Translation>(true);
            var waypoints = GetComponentDataFromEntity<WaypointData>(true);
            
            Entities
                .WithoutBurst()
                .ForEach((ref Translation posData, in CurrentWaypointData wp) =>
            {
                var targetTranslation = translations[wp.entity];

                var dir = posData.Value - targetTranslation.Value;
                if (math.lengthsq(dir) < math.E)
                {
                    wp.entity = waypoints[wp.entity].next;
                }
                else
                {
                    dir = math.normalize(dir) * dt * 5;
                    posData.Value = posData.Value - dir;
                }
            }).Run();
        }
    }
}