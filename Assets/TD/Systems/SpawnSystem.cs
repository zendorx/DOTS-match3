using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TD.Components
{
    public class SpawnSystem : SystemBase
    {
        private float timer;
        private EndSimulationEntityCommandBufferSystem ss;
        protected override void OnCreate()
        {
            base.OnCreate();
            ss = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            timer -= Time.DeltaTime;
            if (timer > 0)
                return;
            timer = 1;

            var entityPrefab = TDMain.instance.unitEntity;


            //var translations = GetComponentDataFromEntity<Translation>(true);
            
            //var cb = ss.CreateCommandBuffer();
            Entities.WithStructuralChanges().ForEach((Entity srcEntity, in StartWaypointTag start) =>
            {
                var unit = EntityManager.Instantiate(entityPrefab);
                
                EntityManager.AddComponentData(unit, new RotateData{speed = 1});
                EntityManager.AddComponentData(unit, new CurrentWaypointData{entity = srcEntity});

                //var tr = translations[srcEntity];
                EntityManager.SetComponentData(unit, new Translation{Value = new float3
                {
                    //x = tr.Value.x + Random.Range(-1, 2),
                    //z = tr.Value.z + Random.Range(-1, 2),
                    x = Random.Range(-1, 2),
                    z = Random.Range(-1, 2),
                }});
            }).Run();
        }
    }
}