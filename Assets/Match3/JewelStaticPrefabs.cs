using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class JewelStaticPrefabs : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public static Entity red;
    public static Entity green;
    public static Entity pink;
    public static Entity blue;
    public static Entity heavy;

    public GameObject redPrefab;
    public GameObject greenPrefab;
    public GameObject pinkPrefab;
    public GameObject bluePrefab;
    public GameObject heavyPrefab;
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        red = conversionSystem.GetPrimaryEntity(redPrefab);
        green = conversionSystem.GetPrimaryEntity(greenPrefab);
        pink = conversionSystem.GetPrimaryEntity(pinkPrefab);
        blue = conversionSystem.GetPrimaryEntity(bluePrefab);
        heavy = conversionSystem.GetPrimaryEntity(heavyPrefab);
    }
    
    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(redPrefab);
        referencedPrefabs.Add(greenPrefab);
        referencedPrefabs.Add(pinkPrefab);
        referencedPrefabs.Add(bluePrefab);
        referencedPrefabs.Add(heavyPrefab);
    }
    // Poor way
    /*public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        using (BlobAssetStore blobAssetStore = new BlobAssetStore())
        {
            red = GameObjectConversionUtility.ConvertGameObjectHierarchy(redPrefab,
                GameObjectConversionSettings.FromWorld(dstManager.World, blobAssetStore));
            
            green = GameObjectConversionUtility.ConvertGameObjectHierarchy(greenPrefab,
                GameObjectConversionSettings.FromWorld(dstManager.World, blobAssetStore));
            
            pink = GameObjectConversionUtility.ConvertGameObjectHierarchy(pinkPrefab,
                GameObjectConversionSettings.FromWorld(dstManager.World, blobAssetStore));
            
            blue = GameObjectConversionUtility.ConvertGameObjectHierarchy(bluePrefab,
                GameObjectConversionSettings.FromWorld(dstManager.World, blobAssetStore));
            
            heavy = GameObjectConversionUtility.ConvertGameObjectHierarchy(heavyPrefab,
                GameObjectConversionSettings.FromWorld(dstManager.World, blobAssetStore));
        }
    }*/
    

    
}
