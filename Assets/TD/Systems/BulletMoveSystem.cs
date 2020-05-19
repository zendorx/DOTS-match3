using Unity.Entities;
using Unity.Transforms;

namespace TD.Components
{
    [DisableAutoCreation]
    public class BulletMoveSystem : MySystem
    { 
        protected override void OnUpdate()
        {
            var cb = createCommandBuffer();
            
            Entities
                .WithoutBurst()
                .ForEach((Entity srcEntity, ref BulletData bulletData, ref TargetReachedData targetReachedData, in Move2TargetData targetData) =>
                {
                    cb.DestroyEntity(srcEntity);
                    cb.AddComponent(targetData.entity, new DeadUnitData());
                    //cb.AddComponent(targetData.entity, new SEtNa);
                }).Run();
        }
    }
}