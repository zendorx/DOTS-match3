using Unity.Collections;
using Unity.Entities;

namespace TD.Components
{
    [DisableAutoCreation]
    [UpdateBefore(typeof(TargetMoveSystem))]
    public class GridSystem : MySystem
    {
        public NativeArray<Entity> grid;
        protected override void OnUpdate()
        {
            var cb = createCommandBuffer();
            
            Entities
                .WithoutBurst()
                .ForEach((Entity srcEntity, in TargetMovePositionData targetData) =>
                {
                    
                }).Run();
        }
    }
}