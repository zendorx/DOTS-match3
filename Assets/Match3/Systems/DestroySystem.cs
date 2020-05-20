using System.Collections;
using System.Collections.Generic;
using TD.Components;
using Unity.Entities;
using UnityEngine;

[DisableAutoCreation]
public class DestroySystem : MySystem
{
    protected override void OnUpdate()
    {
        var cb = createCommandBuffer();
            
        Entities.ForEach((Entity e, ref JewelSelected cp) =>
            {
                cb.DestroyEntity(e);
            }).Run();
    }
}
