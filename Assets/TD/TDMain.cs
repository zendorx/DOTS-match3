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

    //public Entity unitEntity;
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

        var blob = new BlobAssetStore();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(world, blob);
        //unitEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(UnitPrefab, settings);
        bulletEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(BulletPrefab, settings);

        //entities bug workaround
        //https://forum.unity.com/threads/invalidoperationexception-object-is-not-initialized-or-has-already-been-destroyed.882484/
        
        //addSystem<AssignMovePositionSystem>();
        
        
        addSystem<SpawnSystem>();
        addSystem<SpawnShootSystem>();
        addSystem<TargetMoveSystem>();
        addSystem<RotateSystem>();
        addSystem<CollectDamageSystem>();
        addSystem<ApplyDamageSystem>();
        addSystem<WaypointsMoveSystem>();  
        addSystem<BulletMoveSystem>();
        addSystem<AttackBuildingSystem>();
        addSystem<EndReachedSystem>();
        addSystem<DestroyDeadSystem>();
    }

    void addSystem<T>() where T : ComponentSystemBase
    {
        var ssg = world.GetOrCreateSystem<Match3Group>();
        ssg.AddSystemToUpdateList(world.GetOrCreateSystem<T>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
