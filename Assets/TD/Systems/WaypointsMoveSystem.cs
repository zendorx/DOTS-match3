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
            
            
            var translations = GetComponentDataFromEntity<Translation>(true);
            var waypoints = GetComponentDataFromEntity<WaypointData>(true);


            var cb = createCommandBuffer();
            
            Entities
                .WithoutBurst()
                .ForEach((Entity entity, ref Translation posData, in CurrentWaypointData wp) =>
            {
                var targetTranslation = translations[wp.entity];
                
                var dir = posData.Value - targetTranslation.Value;
                if (math.lengthsq(dir) < math.E)
                {
                    var next = waypoints[wp.entity].next;
                    if (next == Entity.Null)
                    {
                        cb.AddComponent(entity, new ReachedEnd());
                    }
                    else
                        wp.entity = next;
                }
                else
                {
                    dir = math.normalize(dir) * dt * 10;
                    posData.Value = posData.Value - dir;
                }
            }).Run();
        }
    }
}