using Unity.Entities;

namespace TD.Components
{
    [GenerateAuthoringComponent]
    public struct ShooterData : IComponentData
    {
        public float timer;
        public float bulletSpeed;
        public float period;
        public float damage;
    }
}