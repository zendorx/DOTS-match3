using Unity.Entities;
using Unity.Transforms;

namespace TD.Components
{
    [DisableAutoCreation]
    public class BulletMoveSystem : MySystem
    { 
        protected override void OnUpdate()
        {
            
            Entities
                .WithStructuralChanges()
                .WithAll<BulletData>()
                .ForEach((Entity srcEntity, 
                    //ref BulletData bulletData, 
                    in TargetReachedData targetReachedData,
                    in Move2TargetData targetData) =>
                {
                    EntityManager.DestroyEntity(srcEntity);
                    //EntityManager.AddComponentData(targetData.entity, new DeadData());
                    //cb.DestroyEntity(entityInQueryIndex, srcEntity);
                    //cb.AddComponent(entityInQueryIndex, targetData.entity, new DeadData());
                    //cb.AddComponent(targetData.entity, new SEtNa);
                }).Run();
            
            //cba.Playback(EntityManager);
            
            //endSimulationSystem.AddJobHandleForProducer(Dependency);
        }
    }
}