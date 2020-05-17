using Unity.Entities;

namespace Match3
{
    [GenerateAuthoringComponent]
    public struct JewelPrefabs : IComponentData
    {
        public Entity red;
        public Entity green;
        public Entity pink;
        public Entity blue;
        public Entity heavy;
    }
}