﻿using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TD.Components
{
    [DisableAutoCreation]
    public class TargetMoveSystem : MySystem
    { 
        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;
            
            
            var translations = GetComponentDataFromEntity<Translation>(true);

            var cb = new EntityCommandBuffer(Allocator.Temp, PlaybackPolicy.SinglePlayback);
            
            
            Entities.WithNone<TargetReachedData>()
                .WithoutBurst()
                .ForEach((Entity entity, ref Translation posData, in Move2TargetData targetData) =>
                {
                    if (!translations.Exists(targetData.entity))
                    {
                        cb.AddComponent(entity, new DeadData());
                        return;
                    }

                    var targetTranslation = translations[targetData.entity];
                
                    var dir = posData.Value - targetTranslation.Value;
                    if (math.length(dir) < 0.01f)
                    {
                        cb.AddComponent(entity, new TargetReachedData());
                    }
                    else
                    {
                        var step = math.normalize(dir) * dt * 10 * targetData.speed;
                        if (math.length(dir) < math.length(step))
                        {
                            posData.Value = targetTranslation.Value;
                            cb.AddComponent(entity, new TargetReachedData());
                        }
                        else
                            posData.Value = posData.Value - step;
                    }
                }).Run();
            
            cb.Playback(EntityManager);
        }
    }
}