using Unity.Entities;

namespace TD.Components
{
    [DisableAutoCreation]
    public class DestroyDeadSystem : MySystem
    { 
        protected override void OnUpdate()
        {
            float dt = Time.DeltaTime;
            Entities
                .WithStructuralChanges()
                .ForEach((Entity srcEntity, ref DeadData deadData) =>
                {
                    deadData.duration -= dt;
                    if (deadData.duration > 0)
                        return;
                    
                    EntityManager.DestroyEntity(srcEntity);
                }).Run();
        }
    }
}