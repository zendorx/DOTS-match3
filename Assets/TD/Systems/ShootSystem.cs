using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace TD.Components
{
    [DisableAutoCreation]
    [UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
    public class ShootSystem : MySystem
    {
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

            Dependency = Entities.ForEach((int entityInQueryIndex, Entity entity, in Translation translation, in ShooterData shooterData) =>
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
                    new Move2TargetData {entity = closestEntity, speed = shooterData.bulletSpeed});
                cb.SetComponent(entityInQueryIndex, bulletEntity, translation);
            }).WithDeallocateOnJobCompletion(targetEntityArray)
                .WithDeallocateOnJobCompletion(translations)
                .ScheduleParallel(Dependency);
            
            endSimulationSystem.AddJobHandleForProducer(Dependency);

        }
    }
}