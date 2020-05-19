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
    [UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
    public class EndReachedSystem : MySystem
    {
        private float timer;
        
        protected override void OnUpdate()
        {
            var cb = createCommandBuffer();

            Entities.WithAll<ReachedEnd>().ForEach((
                Entity entity) =>
            {
                //cb.DestroyEntity(entity);
                
                
            }).Run();
        }
    }
}