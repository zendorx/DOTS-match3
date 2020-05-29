using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace TD.Components
{
    [DisableAutoCreation]
    public class SpawnShootSystem : MySystem
    {
        protected override void OnUpdate()
        {
            var query = new EntityQueryDesc
            {
                None = new ComponentType[]{typeof(DeadData)},
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

            var dt = Time.DeltaTime;

            Entities.WithBurst().ForEach((int entityInQueryIndex, Entity entity, 
                ref ShooterData shooterData, in Translation translation) =>
                {
                    shooterData.timer -= dt;
                    if (shooterData.timer > 0)
                        return;
                    shooterData.timer = shooterData.period/10;
                    
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
                    
                    if (closestDistance > shooterData.range)
                        return;

                    var bulletEntity = cb.Instantiate(entityInQueryIndex, bulletPrefabEntity);
                    cb.AddComponent(entityInQueryIndex, bulletEntity, new BulletData{damage = shooterData.damage});
                    cb.AddComponent(entityInQueryIndex, bulletEntity,
                        new Move2TargetData {entity = closestEntity, speed = shooterData.bulletSpeed, voffset = 1.5f});
                    cb.SetComponent(entityInQueryIndex, bulletEntity, translation);
            }).WithDeallocateOnJobCompletion(targetEntityArray)
                .WithDeallocateOnJobCompletion(translations)
                .ScheduleParallel();
            
            endSimulationSystem.AddJobHandleForProducer(Dependency);

        }
    }
}