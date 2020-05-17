using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FallSystem : SystemBase
{
    public EntityCommandBuffer.Concurrent CommandBuffer;
    public const int JobIndex = 10001;
    
    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        float reachedPositionDistance = 0.05f;
        

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
                //CommandBuffer.RemoveComponent<FallComponent>(JobIndex, e);
            }
        }).Run();
    }
}
