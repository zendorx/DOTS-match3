using Unity.Entities;

namespace TD.Components
{
    public abstract class MySystem : SystemBase
    {
        protected EndSimulationEntityCommandBufferSystem endSimulationSystem;
        protected override void OnCreate()
        {
            endSimulationSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            xCreate();
        }

        protected virtual void xCreate(){}

        protected EntityCommandBuffer createCommandBuffer()
        {
            return endSimulationSystem.CreateCommandBuffer();
        }
    }
}