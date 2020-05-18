using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace TD.Components
{
    [DisableAutoCreation]
    public class AssignMovePositionSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;
            //var waypointsQuery = GetEntityQuery(ComponentType.ReadOnly<WaypointData>(), 
            //    ComponentType.ReadOnly<Translation>());
            
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
                    var next = waypoints[wp.entity].next;
                    if (next != Entity.Null)
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