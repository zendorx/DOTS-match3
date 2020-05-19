using System.Collections;
using System.Collections.Generic;
using TD.Components;
using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(SelectSystem))]
public class ClickClearSystem : MySystem
{
    protected override void OnUpdate()
    {
        var cb = createCommandBuffer();
            
        Entities.ForEach((Entity e, ref ClickPoint cp) =>
        {
            cb.DestroyEntity(e);
        }).Run();
    }
}
