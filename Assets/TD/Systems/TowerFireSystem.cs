using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TD.Components
{

    public struct TestShader : ISharedComponentData
    {
        public Entity entity;
    }
    
    [DisableAutoCreation]
    public class TowerFireSystem : MySystem
    {
        private EntityQuery enemiesQuery;
        protected override void xCreate()
        {
            var query = new EntityQueryDesc
            {
                None = new ComponentType[]{typeof(DeadUnitData)},
                All = new ComponentType[]{
                    ComponentType.ReadOnly<EnemyData>(), 
                    ComponentType.ReadOnly<Translation>(), 
                    //ComponentType.ReadOnly<TestShader>()
                    
                }
            };

            enemiesQuery = GetEntityQuery(query);
        }

        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;
            
            //enemiesQuery.SetSharedComponentFilter(new TestShader{entity = Entity.Null});
            
            var enemyEntities = enemiesQuery.ToEntityArray(Allocator.TempJob);

            if (enemyEntities.Length == 0)
            {
                enemyEntities.Dispose();
                return;
            }



            var cb = createCommandBuffer();

            //enemiesQuery.filter
            //var enemiesTr = enemiesQuery.ToComponentDataArray<Translation>(Allocator.Temp);

            var bulletEntity = TDMain.instance.bulletEntity;
            Entities
                .ForEach((Entity srcEntity, ref TowerData towerData, in Translation tr) =>
                {
                    /*
                    towerData.timer -= dt;
                    if (towerData.timer > 0)
                        return;

                    Entity closestEntity = Entity.Null;
                    float closestDistance = 9999999f;


                    var towerPos = tr.Value;
                    Entities
                        .ForEach((Entity enemyEntity,in EnemyData enemyData, in Translation trEnemy) =>
                        {
                            var dir = towerPos - trEnemy.Value;
                            var dist = math.length(dir);
                            if (dist > closestDistance)
                                return;
                            closestDistance = dist;
                            closestEntity = enemyEntity;
                        });
                    
                    if (closestEntity == Entity.Null)
                        return;
                    

                    towerData.timer = towerData.period;
                    var entity = cb.Instantiate(bulletEntity);
                    cb.AddComponent(entity, new BulletData());
                    cb.AddComponent(entity, new Move2TargetData{entity = closestEntity, speed = towerData.bulletSpeed});
                    cb.SetComponent(entity, tr);
                    //cb.AddComponent(entity, new b);
                    */

                }).Run();

            enemyEntities.Dispose();
        }
    }
}