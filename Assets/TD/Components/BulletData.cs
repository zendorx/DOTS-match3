using Unity.Entities;

namespace TD.Components
{
    public struct BulletData : IComponentData
    {
        public float damage;
    }
    
    public struct MoveColliderWithUnit : IComponentData
    {
        public float damage;
    }
}