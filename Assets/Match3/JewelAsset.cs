using Unity.Entities;

namespace Match3
{
    public struct JewelAsset
    {
        public Entity entity;
    }

    public struct JewelBlobAsset
    {
        public BlobArray<JewelAsset> jewelArray;
    }
    
}