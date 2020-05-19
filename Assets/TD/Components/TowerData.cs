using Unity.Entities;

namespace TD.Components
{
    [GenerateAuthoringComponent]
    public struct TowerData : IComponentData
    {
        public float timer;
        public float bulletSpeed;
        public float period;
        public float damage;
    }
}