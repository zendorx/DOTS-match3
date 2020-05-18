using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SelectSystem : SystemBase
{
    protected EndSimulationEntityCommandBufferSystem ess;
    
    protected override void OnCreate()
    {
        base.OnCreate();
        ess = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

    }

    protected override void OnUpdate()
    {
        EntityQuery eq = GetEntityQuery(ComponentType.ReadOnly<ClickPoint>());
        
        var ecb = ess.CreateCommandBuffer();

        using (var clickPoints = eq.ToComponentDataArray<ClickPoint>(Allocator.Temp))
        {
            if (clickPoints.Length == 0)
                return;

            ClickPoint click = clickPoints[0];
            int2 clickIndex = SpawnerMono.PositionToIndex(click.position);
            
            Entities.ForEach((Entity e, in JewelCell cell) =>
            {
                if (cell.x == clickIndex.x && cell.y == clickIndex.y)
                {
                    ecb.AddComponent(e, new JewelSelected());
                }
                
            }).Run();
            
        }
    }
}
