using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.SceneManagement;
using UnityEngine;

[DisableAutoCreation]
public class FallSystem : SystemBase
{
    public const int JobIndex = 10001;
    
    protected EndSimulationEntityCommandBufferSystem m_EndSimulationEcbSystem;
    
    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        float reachedPositionDistance = 0.05f;

        var ecb = m_EndSimulationEcbSystem.CreateCommandBuffer();


        Entities.ForEach((Entity e, ref Translation t, in FallComponent f) =>
        {
            if (math.distance(t.Value, f.position) > reachedPositionDistance)
            {
                float3 moveDir = math.normalize(f.position - t.Value);
                t.Value += moveDir * f.speed * dt;
            }
            else
            {
                t.Value = f.position;
                //World.DefaultGameObjectInjectionWorld.EntityManager.RemoveComponent<FallComponent>(e);
                ecb.RemoveComponent<FallComponent>(e);
            }
        }).Run();

        
    }
}
