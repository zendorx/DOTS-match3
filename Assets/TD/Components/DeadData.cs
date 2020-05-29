using Unity.Entities;

namespace TD.Components
{
    public struct DeadData : IComponentData
    {
        public float duration;
    }

    public struct ApplyDamageData : IBufferElementData
    {
        public float value;
    }
}