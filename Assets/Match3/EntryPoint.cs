using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public class Match3Group : ComponentSystemGroup
{
 
}


public class EntryPoint : MonoBehaviour
{
    static public EntryPoint instance;
    
    public int width;
    public int height;

    public float cellWidth;
    public float cellHeight;

    public float cellSpace;
    
    private World world;
    private EntityManager em;

    private void Awake()
    {
        instance = this;
        world = World.DefaultGameObjectInjectionWorld;
        em = world.EntityManager;
        
        addSystem<Match3InputSystem>();
        addSystem<SelectSystem>();
        addSystem<JewelSpawnSystem>();
        addSystem<FallDetectSystem>();
        addSystem<FallSystem>();
        
        addSystem<DestroySystem>();
        addSystem<ClickClearSystem>();

    }
    
    void addSystem<T>() where T : ComponentSystemBase
    {
        var ssg = world.GetOrCreateSystem<Match3Group>();
        ssg.AddSystemToUpdateList(world.GetOrCreateSystem<T>());
    }

}
