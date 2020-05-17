using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SpawnerMono : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private Entity entityPrefab;
    private World world;
    private EntityManager em;
    
    void Start()
    {
        em = World.DefaultGameObjectInjectionWorld.EntityManager;
        world = World.DefaultGameObjectInjectionWorld;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(world, null);

        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, settings);
        
        //InstantiateEntity(new float3(1f, 0, 0f));
        InstantiateGrid(10, 10, 1.1f);
    }

    public void InstantiateGrid(int width, int height, float spacing = 1f)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                InstantiateEntity(new float3(i * spacing, j * spacing, 0));
            }
        }
    }
    
    public void InstantiateEntity(float3 position)
    {
        Entity e = em.Instantiate(entityPrefab);
        
        em.SetComponentData(e, new Translation
        {
            Value = position
        });
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
