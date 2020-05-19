using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TD.Components
{
    [DisableAutoCreation]
    public class TowerFireSystem : MySystem
    {
        private EntityQuery enemiesQuery;
        protected override void xCreate()
        {
            var query = new EntityQueryDesc
            {
                None = new ComponentType[]{typeof(DeadUnitData)},
                All = new ComponentType[]{ComponentType.ReadOnly<EnemyData>(), ComponentType.ReadOnly<Translation>()}
            };
            
            enemiesQuery = GetEntityQuery(query);
        }

        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;
            
            
            var enemyEntities = enemiesQuery.ToEntityArray(Allocator.TempJob);
            
            if (enemyEntities.Length == 0)
                return;
            
            var cb = createCommandBuffer();

            //enemiesQuery.filter
            //var enemiesTr = enemiesQuery.ToComponentDataArray<Translation>(Allocator.Temp);
            
            Entities
                .WithoutBurst()
                .ForEach((Entity srcEntity, ref TowerData towerData, in Translation tr) =>
                {
                    towerData.timer -= dt;
                    if (towerData.timer > 0)
                        return;
                    
                    
                    
                    
                    towerData.timer = towerData.period;
                    var entity = cb.Instantiate(TDMain.instance.bulletEntity);
                    cb.AddComponent(entity, new BulletData());
                    cb.AddComponent(entity, new Move2TargetData{entity = enemyEntities[0], speed = towerData.bulletSpeed});
                    cb.SetComponent(entity, tr);
                    //cb.AddComponent(entity, new b);

                }).Run();

            enemyEntities.Dispose();
        }
    }
}