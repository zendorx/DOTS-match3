using Unity.Entities;

namespace TD.Components
{
    public abstract class MySystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem ss;
        protected override void OnCreate()
        {
            ss = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            xCreate();
        }

        protected virtual void xCreate(){}

        protected EntityCommandBuffer createCommandBuffer()
        {
            return ss.CreateCommandBuffer();
        }
    }
}