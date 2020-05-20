using System;
using System.Collections;
using System.Collections.Generic;
using TD.Components;
using Unity.Entities;
using UnityEngine;

public class TDMain : MonoBehaviour
{
    static public  TDMain instance;
    public GameObject UnitPrefab;
    public GameObject BulletPrefab;

    public Entity unitEntity;
    public Entity bulletEntity;


    private World world;
    //private Entity UnitEntity; 
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //UnityEntity = ConvertToEntity(UnitPrefab);
        
        var em = World.DefaultGameObjectInjectionWorld.EntityManager;
        world = World.DefaultGameObjectInjectionWorld;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(world, null);
        unitEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(UnitPrefab, settings);
        bulletEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(BulletPrefab, settings);

        //entities bug workaround
        //https://forum.unity.com/threads/invalidoperationexception-object-is-not-initialized-or-has-already-been-destroyed.882484/
        
        //addSystem<AssignMovePositionSystem>();
        addSystem<EndReachedSystem>();
        addSystem<RotateSystem>();
        addSystem<SpawnSystem>();
        addSystem<TowerFireSystem>();
        addSystem<TargetMoveSystem>();
        addSystem<WaypointsMoveSystem>();
        addSystem<BulletMoveSystem>();
        addSystem<GridSystem>();
        
        var ssg = world.GetOrCreateSystem<SimulationSystemGroup>();
        ssg.SortSystemUpdateList();
    }

    void addSystem<T>() where T : ComponentSystemBase
    {
        var ssg = world.GetOrCreateSystem<SimulationSystemGroup>();
        ssg.AddSystemToUpdateList(world.GetOrCreateSystem<T>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
