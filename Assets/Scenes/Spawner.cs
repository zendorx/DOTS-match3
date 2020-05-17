using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Mesh unitMesh;
    [SerializeField] private Material unitMaterial;
    [SerializeField] private GameObject gameObjectPRefab;

    [SerializeField] private int DimensionX;
    [SerializeField] private int DimensionY;
    
    [Range(0, 2)]
    [SerializeField] private float Spacing;
    private Entity _entity;
    private World _world;
    private EntityManager _manager;
    
    void Start()
    {
        _world = World.DefaultGameObjectInjectionWorld.EntityManager.World;
        _manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        
        GameObjectConversionSettings settings = new GameObjectConversionSettings(_world, GameObjectConversionUtility.ConversionFlags.AddEntityGUID);
        _entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPRefab, settings);

        InstantiateEntityGrid(DimensionX, DimensionY, Spacing);

    }

    private void InstantiateEntity(float3 position)
    {
        Entity entity = _manager.Instantiate(_entity);
        
        _manager.SetComponentData(entity, new Translation {Value = position});
    }

    private void InstantiateEntityGrid(int dimX, int dimY, float spacing = 1f)
    {
        for (int i = 0; i < dimX; i++)
        {
            for (int j = 0; j < dimY; j++)
            {
                InstantiateEntity(new float3(i * spacing, j * spacing, 0));
            }
            
        }
    }

    private void MakeEntity()
    {
        EntityManager em = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype archetype = em.CreateArchetype(
            typeof(Translation),
            typeof(RenderMesh),
            typeof(Rotation),
            typeof(RenderBounds),
            typeof(LocalToWorld)
        );

        var entity = em.CreateEntity(archetype);
        
        em.SetComponentData(entity, new Translation
        {
            Value = new float3(2f, 0f, 4f)
        });

        em.SetSharedComponentData(entity, new RenderMesh
        {
            mesh = unitMesh,
            material = unitMaterial
        });
        
    }

}
