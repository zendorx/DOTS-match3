using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public class Match3InputSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float3 clickPosition = UtilsClass.GetMouseWorldPosition();

            var entity = World.EntityManager.CreateEntity(typeof(ClickPoint));
            
            World.EntityManager.SetComponentData(entity, new ClickPoint
            {
                position = clickPosition
            });
        }
    }
}
