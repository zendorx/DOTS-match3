using Unity.Entities;
using Unity.Transforms;

namespace TD.Components
{
    [DisableAutoCreation]
    public class BulletMoveSystem : MySystem
    { 
        protected override void OnUpdate()
        {
            var cb = createCommandBuffer().ToConcurrent();
            
            Entities
                .WithoutBurst()
                .ForEach((int entityInQueryIndex, Entity srcEntity, ref BulletData bulletData, ref TargetReachedData targetReachedData, in Move2TargetData targetData) =>
                {
                    cb.DestroyEntity(entityInQueryIndex, srcEntity);
                    cb.AddComponent(entityInQueryIndex, targetData.entity, new DeadUnitData());
                    //cb.AddComponent(targetData.entity, new SEtNa);
                }).ScheduleParallel();
            
            endSimulationSystem.AddJobHandleForProducer(Dependency);
        }
    }
}