using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TD.Components
{
    [DisableAutoCreation]
    public class SpawnSystem : SystemBase
    {
        private float timer;
        
        protected override void OnCreate()
        {
            base.OnCreate();
        }

        protected override void OnUpdate()
        {
            timer -= Time.DeltaTime;
            if (timer > 0)
                return;
            timer = 0.01f;

            var entityPrefab = TDMain.instance.unitEntity;

            Entities.WithStructuralChanges().ForEach((
                Entity srcEntity, 
                in StartWaypointTag start, 
                in Translation tr) =>
            {
                
                var unit = EntityManager.Instantiate(entityPrefab);
                
                EntityManager.AddComponentData(unit, new RotateData{speed = 1});
                EntityManager.AddComponentData(unit, new CurrentWaypointData{entity = srcEntity});

                EntityManager.SetComponentData(unit, new Translation{Value = new float3
                {
                    x = tr.Value.x + Random.Range(-1, 2),
                    z = tr.Value.z + Random.Range(-1, 2)
                }});
            }).Run();

            //this.Enabled = false;
        }
    }
}