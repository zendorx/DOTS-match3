using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
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

    [DisableAutoCreation]
    public class TowerFireJobSystem : MySystem
    {
        private struct EntityWithPosition
        {
            public Entity entity;
            public float3 position;
        }



        [RequireComponentTag(typeof(TowerData))]
        private struct FindTargetJob: IJobForEachWithEntity<Translation>
        {
            [DeallocateOnJobCompletion]
            [ReadOnly]
            public NativeArray<EntityWithPosition> targetArray;
            public EntityCommandBuffer.Concurrent entityCommandBuffer;

            [ReadOnly]
            public Entity bulletPrefabEntity;
            
            public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation
                //, ref TowerData towerData
                )
            {
                
                
            }
        }
        protected override void OnUpdate()
        {
            var query = new EntityQueryDesc
            {
                None = new ComponentType[]{typeof(DeadUnitData)},
                All = new ComponentType[]{
                    ComponentType.ReadOnly<EnemyData>(), 
                    ComponentType.ReadOnly<Translation>()
                }
            };

            var enemiesQuery = GetEntityQuery(query);
            
            var targetEntityArray = enemiesQuery.ToEntityArray(Allocator.TempJob);
            var translations = enemiesQuery.ToComponentDataArray<Translation>(Allocator.TempJob);
            
            var cb = createCommandBuffer().ToConcurrent();
            var bulletPrefabEntity = TDMain.instance.bulletEntity;

            var j1 = Entities.ForEach((int entityInQueryIndex, Entity entity, in Translation translation, in TowerData towerData) =>
            {
                Entity closestEntity = Entity.Null;
                float closestDistance = 9999999f;

                var towerPos = translation.Value;
                for (int i = 0; i < targetEntityArray.Length; ++i)
                {
                    var dir = towerPos - translations[i].Value;
                    var dist = math.length(dir);
                    if (dist > closestDistance)
                        continue;
                    closestDistance = dist;
                    closestEntity = targetEntityArray[i];
                }

                if (closestEntity == Entity.Null)
                    return;

                var bulletEntity = cb.Instantiate(entityInQueryIndex, bulletPrefabEntity);
                cb.AddComponent(entityInQueryIndex, bulletEntity, new BulletData());
                cb.AddComponent(entityInQueryIndex, bulletEntity,
                    new Move2TargetData {entity = closestEntity, speed = towerData.bulletSpeed});
                cb.SetComponent(entityInQueryIndex, bulletEntity, translation);
            }).ScheduleParallel(Dependency);
            
            j1.Complete();

            targetEntityArray.Dispose();
            translations.Dispose();
        }
    }
}