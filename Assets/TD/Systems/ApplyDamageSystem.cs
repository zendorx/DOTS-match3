using Unity.Collections;
using Unity.Entities;

namespace TD.Components
{
    [DisableAutoCreation]
    public class ApplyDamageSystem : MySystem
    {
        private float timer;
        
        protected override void OnUpdate()
        {
            var cb = new EntityCommandBuffer(Allocator.Temp, PlaybackPolicy.SinglePlayback);
            var damages = GetBufferFromEntity<ApplyDamageData>();
            
            Entities.WithNone<DeadData>().ForEach((Entity entity, ref EnemyData enemyData) =>
            {
                var buf = damages[entity];
                var value = 0f;
                for (int i = 0; i < buf.Length; ++i)
                {
                    value += buf[i].value;
                }
                
                //buf.Clear();
                enemyData.hp -= value;
                if (enemyData.hp <= 0)
                {
                    //cb.AddComponent(entity, new DeadData());
                    cb.DestroyEntity(entity);
                }
            }).Run();
            
            cb.Playback(EntityManager);
        }
    }
}