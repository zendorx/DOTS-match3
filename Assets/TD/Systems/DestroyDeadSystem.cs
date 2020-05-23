using Unity.Entities;

namespace TD.Components
{
    [DisableAutoCreation]
    public class DestroyDeadSystem : MySystem
    { 
        protected override void OnUpdate()
        {
            
            Entities
                .WithStructuralChanges()
                .WithAll<DeadData>()
                .ForEach((Entity srcEntity) =>
                {
                    EntityManager.DestroyEntity(srcEntity);
                }).Run();
        }
    }
}