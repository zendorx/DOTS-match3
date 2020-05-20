using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public class Match3Group : ComponentSystemGroup
{
    public void AddSystem(ComponentSystemBase sys)
    {
        if (sys != null)
        {
            if (this == sys)
                throw new ArgumentException($"Can't add {TypeManager.GetSystemName(GetType())} to its own update list");

            // Check for duplicate Systems. Also see issue #1792
            if (m_systemsToUpdate.IndexOf(sys) >= 0)
                return;

            m_systemsToUpdate.Add(sys);
        }
    }
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
        Match3Group ssg = world.GetOrCreateSystem<Match3Group>();
        ssg.AddSystem(world.GetOrCreateSystem<T>());
    }

}
