using System.Collections;
using System.Collections.Generic;
using TD.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

[UpdateAfter(typeof(FallDetectSystem))]
public class JewelSpawnSystem : MySystem
{
    protected override void OnUpdate()
    {
        var cb = createCommandBuffer();

        Entities.WithoutBurst().ForEach((Entity e, in SpawnNeedComponent spawn) =>
         {
             Entity jewelEntity = cb.Instantiate(JewelStaticPrefabs.heavy);

             float3 position = SpawnerMono.IndexToPosition(spawn.x, spawn.y);
             
             cb.AddComponent(jewelEntity, new Translation
             {
                 Value = new float3(position.x,  spawn.y * (SpawnerMono.spacing + 1.2f), 0)
             });
             
             cb.AddComponent(jewelEntity, new FallComponent
             {
                 position = position,
                 speed = 10
             });
             
             cb.AddComponent(jewelEntity, new JewelCell
             {
                 x = spawn.x,
                 y = spawn.y
             });
             
             cb.DestroyEntity(e);
         }).Run();//TODO: pass inputDeps to cb

        
    }
}
