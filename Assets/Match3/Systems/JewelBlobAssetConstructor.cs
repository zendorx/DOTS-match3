using Unity.Collections;
using Unity.Entities;

namespace Match3
{

    /*[UpdateInGroup(typeof(GameObjectAfterConversionGroup))]
    public class JewelBlobAssetConstructor : GameObjectConversionSystem
    {
        protected override void OnUpdate()
        {
            BlobAssetReference<JewelBlobAsset> blobAssetReference;
                
            using (BlobBuilder blobBuilder = new BlobBuilder(Allocator.Temp))
            {
                ref var jewelBlobAsset = ref blobBuilder.ConstructRoot<JewelBlobAsset>();
                var blobBuilderArray = blobBuilder.Allocate(ref jewelBlobAsset.jewelArray, 5);
//fill
                blobAssetReference = blobBuilder.CreateBlobAssetReference<JewelBlobAsset>(Allocator.Persistent);
            }

            EntityQuery jewelCreatorQuery = DstEntityManager.CreateEntityQuery(typeof(JewelCreatorData));
            Entity jewelCreatorEntity = jewelCreatorQuery.GetSingletonEntity();

            DstEntityManager.AddComponentData(jewelCreatorEntity, new JewelCreatorData
            {
                jewels = blobAssetReference
            });
        }
    }*/
}