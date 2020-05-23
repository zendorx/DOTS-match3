using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TD.Components
{
    [DisableAutoCreation]
    public class EndReachedSystem : MySystem
    {
        private float timer;
        
        protected override void OnUpdate()
        {
            var commandBuffer = new EntityCommandBuffer(Allocator.Temp, PlaybackPolicy.SinglePlayback);

            Entities.WithAll<ReachedEnd>().ForEach((
                Entity entity) =>
            {
                //commandBuffer.DestroyEntity(entity);
            }).ScheduleParallel();
            Dependency.Complete();
            commandBuffer.Playback(EntityManager);
        }
    }
}