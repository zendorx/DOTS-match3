using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TD.Components
{
    [DisableAutoCreation]
    [UpdateAfter(typeof(WaypointsMoveSystem))]
    public class EndReachedSystem : MySystem
    {
        private float timer;
        
        protected override void OnUpdate()
        {

            var cb = createCommandBuffer();

            var entityPrefab = TDMain.instance.unitEntity;

            Entities.WithAll<ReachedEnd>().ForEach((
                Entity entity) =>
            {
                cb.DestroyEntity(entity);
            }).Run();
        }
    }
}