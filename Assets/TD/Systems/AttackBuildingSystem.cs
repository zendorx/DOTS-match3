using Unity.Entities;

namespace TD.Components
{
    [DisableAutoCreation]
    public class AttackBuildingSystem : MySystem
    { 
        protected override void OnUpdate()
        {
            
            Entities
                .WithStructuralChanges()
                .WithAll<ReachedEnd>()
                .WithAll<EnemyData>()
                .ForEach((Entity entity, 
                    //ref BulletData bulletData, 
                    in TargetReachedData targetReachedData,
                    in Move2TargetData targetData) =>
                {
                    EntityManager.AddComponentData(entity, new AttackingData());
                }).Run();
            
            //cba.Playback(EntityManager);
            
            //endSimulationSystem.AddJobHandleForProducer(Dependency);
        }
    }
}