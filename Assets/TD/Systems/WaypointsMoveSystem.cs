using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace TD.Components
{
    [DisableAutoCreation]
    public class WaypointsMoveSystem : MySystem
    { 
        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;

            var waypoints = GetComponentDataFromEntity<WaypointData>(true);

            var cb = createCommandBuffer();
            
            Entities
                .WithAll<EnemyData>()
                .WithoutBurst()
                .ForEach((Entity entity, ref Move2TargetData targetData, in TargetReachedData targetReachedData) =>
            {
                var next = waypoints[targetData.entity].next;
                if (next == Entity.Null)
                {
                    cb.AddComponent(entity, new ReachedEnd());
                }
                else
                {
                    targetData.entity = next;
                    cb.RemoveComponent(entity, typeof(TargetReachedData));
                }
            }).Run();
        }
    }
}