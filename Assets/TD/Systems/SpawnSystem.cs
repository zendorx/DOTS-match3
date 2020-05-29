using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TD.Components
{
    [DisableAutoCreation]
    public class SpawnSystem : SystemBase
    {
        protected override void OnCreate()
        {
            Entities.WithStructuralChanges().ForEach((Entity entity, WaypointData wp) =>
            {
                EntityManager.RemoveComponent<RenderMesh>(entity);
            }).Run();
        }

        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;

            //var entityPrefab = TDMain.instance.unitEntity;

            Entities.WithStructuralChanges().ForEach((
                Entity srcEntity, 
                ref StartWaypointTag start, 
                in WaypointData wp,
                in Translation tr) =>
            {

                start.timer -= dt;
                if (start.timer > 0)
                    return;
                start.timer = start.spawnTime/2;

                var unitEntityController = GameObject.Instantiate(TDMain.instance.UnitPrefab).GetComponent<UnitEntityController>();
                var unit = unitEntityController.entity;
                
                EntityManager.AddComponentData(unit, new RotateData{speed = 1, angle = Random.Range(0f, 3.14f)});
                EntityManager.AddComponentData(unit, new Move2TargetData {entity = wp.next, speed = start.moveSpeed/3, voffset = -0.5f}); 
                EntityManager.AddComponentData(unit, new EnemyData{hp = start.hp});
                EntityManager.AddBuffer<ApplyDamageData>(unit);

                EntityManager.SetComponentData(unit, new Translation{Value = new float3
                {
                    x = tr.Value.x + Random.Range(-1, 2),
                    y = tr.Value.y - 0.5f,
                    z = tr.Value.z + Random.Range(-1, 2)
                }});
            }).Run();

            //this.Enabled = false;
        }
    }
}