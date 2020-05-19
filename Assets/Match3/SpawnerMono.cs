using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerMono : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public static float spacing = 1.1f;

    private Entity entityPrefab;
    private World world;
    private EntityManager em;

    public static float3 IndexToPosition(int x, int y)
    {
        return new float3(x * spacing, - y * spacing, 0f);
    }

    public static int2 PositionToIndex(float3 position)
    {
        return new int2((int) (position.x / spacing), (int) (- position.y / spacing));
    }
    
    void Start()
    {
        em = World.DefaultGameObjectInjectionWorld.EntityManager;
        world = World.DefaultGameObjectInjectionWorld;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(world, null);

        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, settings);
        
        //InstantiateEntity(new float3(1f, 0, 0f));
        InstantiateGrid(10, 10);
    }

    public void InstantiateGrid(int width, int height)
    {
        InstantiateFieldEntity(width, height);
            
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Spawn(i, j);
            }
        }
    }

    public void Spawn(int x, int y)
    {
        Entity e = em.CreateEntity();

        em.AddComponentData(e, new SpawnNeedComponent
        {
            x = x,
            y = y
        });
    }

    public void InstantiateFieldEntity(int width, int height)
    {
        Entity e = em.CreateEntity();

        em.AddComponentData(e, new FieldComponent
        {
            width = width,
            height = height
        });
    }
    
    public void InstantiateEntity(int x, int y)
    {
        float3 position = IndexToPosition(x, y);
        
        Entity e = em.Instantiate(entityPrefab);
        
        em.SetComponentData(e, new Translation
        {
            Value = new float3(position.x,  y * (spacing * Random.Range(1.3f, 1.5f)), 0)
        });

        em.AddComponentData(e, new FallComponent
        {
            position = position,
            speed = 10
        });

        em.AddComponentData(e, new JewelCell
        {
            x = x,
            y = y
        });
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
