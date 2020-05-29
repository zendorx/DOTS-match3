using Unity.Entities;

namespace TD.Components
{
    [GenerateAuthoringComponent]
    public struct Move2TargetData : IComponentData
    {
        public Entity entity;
        public float speed;
        public float voffset;
    }
}