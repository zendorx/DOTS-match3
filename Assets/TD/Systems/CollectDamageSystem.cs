using Unity.Collections;
using Unity.Entities;

namespace TD.Components
{
    [DisableAutoCreation]
    public class CollectDamageSystem : MySystem
    {
        private float timer;
        
        protected override void OnUpdate()
        {
            var cb = new EntityCommandBuffer(Allocator.Temp, PlaybackPolicy.SinglePlayback);
            
            var damages = GetBufferFromEntity<ApplyDamageData>();
            
            Entities.WithoutBurst().WithAll<TargetReachedData>().WithNone<DeadData>().ForEach((
                Entity entity, in BulletData bulletData, in Move2TargetData targetData) =>
            {
                if (!EntityManager.Exists(targetData.entity))
                    return;
                
                cb.AddComponent(entity, new DeadData{duration = 0});
                damages[targetData.entity].Add(new ApplyDamageData{value = bulletData.damage});
            }).Run();
            
            cb.Playback(EntityManager);
        }
    }
}