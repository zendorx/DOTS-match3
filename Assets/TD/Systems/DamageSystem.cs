using Unity.Entities;

namespace TD.Components
{
    [DisableAutoCreation]
    public class DamageSystem : MySystem
    {
        private float timer;
        
        protected override void OnUpdate()
        {
            var cb = createCommandBuffer();

            var eq = GetComponentDataFromEntity<EnemyData>();
            Entities.WithAll<TargetReachedData>().WithNone<DeadData>().ForEach((
                Entity entity, in BulletData bulletData, in Move2TargetData targetData) =>
            {
                var enemyData = eq[targetData.entity]; 
                enemyData.hp -= bulletData.damage;
                if (enemyData.hp > 0)
                    return;
                cb.AddComponent(targetData.entity, new DeadData());
                
            }).Run();
        }
    }
}