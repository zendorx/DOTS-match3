using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Authoring;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

namespace TD.Components
{
    [DisableAutoCreation]
    public class MakeDynamicBodySystem : MySystem
    { 
        protected override void OnUpdate()
        {
            
            Entities
                .WithStructuralChanges()
                .WithAll<BulletData>()
                .ForEach((Entity srcEntity,
                    ref PhysicsMass ms,
                    ref PhysicsGravityFactor gravityFactor,
                    in TargetReachedData targetReachedData,
                    in Move2TargetData targetData) =>
                {
                    
                    var r = PhysicsMass.CreateDynamic(MassProperties.UnitSphere, 1);
                    ms.Transform = r.Transform;
                    ms.InverseInertia = r.InverseInertia;
                    ms.InverseMass = r.InverseMass;
                    ms.AngularExpansionFactor = r.AngularExpansionFactor;


                    gravityFactor.Value = 1;


                    //EntityManager.AddComponentData(srcEntity, new PhysicsVelocity());
                    //EntityManager.AddComponentData(targetData.entity, new DeadData());
                    //cb.DestroyEntity(entityInQueryIndex, srcEntity);
                    //cb.AddComponent(entityInQueryIndex, targetData.entity, new DeadData());
                    //cb.AddComponent(targetData.entity, new SEtNa);
                }).Run();
            
            //cba.Playback(EntityManager);
            
            //endSimulationSystem.AddJobHandleForProducer(Dependency);
        }
    }
}