using System;
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
            
            //EntityManager.SetSharedComponentData();

            Entities.WithNone<TargetReachedData>()
                .WithNone<DeadData>()
                .WithoutBurst()
                .ForEach((Entity entity, ref Translation posData, ref Rotation rotation, in Move2TargetData targetData) =>
                {
                    if (!translations.Exists(targetData.entity))
                    {
                        cb.AddComponent(entity, new DeadData{duration = 1.5f});
                        return;
                    }

                    var targetTranslation = translations[targetData.entity].Value;

                    targetTranslation.y += targetData.voffset;
                    

                    var dir = posData.Value - targetTranslation;
                    if (math.length(dir) < 0.01f)
                    {
                        cb.AddComponent(entity, new TargetReachedData());
                    }
                    else
                    {
                        var norm = math.normalize(dir);
                        var step = norm * dt * 10 * targetData.speed;
                        
                        if (math.length(dir) < math.length(step))
                        {
                            posData.Value = targetTranslation;
                            cb.AddComponent(entity, new TargetReachedData());
                        }
                        else
                        {
                            posData.Value = posData.Value - step;
                            
                            var rot = quaternion.LookRotation(-norm, new float3(0,1,0));
                            //var face = quaternion.Euler(0,0,math.PI/2);
                            //var face = quaternion.Euler(-math.PI/2, math.PI/2, math.PI/2);
                            //var face = quaternion.Euler(0,0,math.PI/2);
                            //rot = quaternion.AxisAngle(norm, 0);
                        
                            //rot = math.mul(rot, z);
                            //quaternion.LookRotation()
                            //rotation.Value = math.mul(face, rot);
                            rotation.Value = rot;
                        }
                    }
                }).Run();
            
            cb.Playback(EntityManager);
        }
    }
}