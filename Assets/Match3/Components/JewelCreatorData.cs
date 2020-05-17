using Unity.Entities;

namespace Match3
{
    public struct JewelCreatorData : IComponentData
    {
        public BlobAssetReference<JewelBlobAsset> jewels;
    }
}