using Unity.Entities;

namespace TD.Components
{
    public struct Move2TargetData : IComponentData
    {
        public Entity entity;
        public float speed;
        public float voffset;
    }
}