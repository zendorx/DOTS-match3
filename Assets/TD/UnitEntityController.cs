using System;
using TD.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace TD
{
    public class UnitEntityController : MonoBehaviour
    {
        public Animator Animator;
        
        public Entity entity { get; set; }
        private EntityManager em;

        private void Awake()
        {
            em = World.DefaultGameObjectInjectionWorld.EntityManager;
            entity = em.CreateEntity();
            em.AddComponentData(entity, new Translation());
            em.AddComponentData(entity, new Rotation());
        }

        private void Update()
        {
            if (!em.Exists(entity))
            {
                Destroy(gameObject);
                return;
            }
            var translation = em.GetComponentData<Translation>(entity);
            transform.position = translation.Value;
            
            var rotation = em.GetComponentData<Rotation>(entity);
            transform.rotation = rotation.Value;
            
            

            if (em.HasComponent<DeadData>(entity))
            {
                Animator.Play("death");
            }
            else
            {
                var attacking = em.HasComponent<AttackingData>(entity);
                if (attacking)
                {
                    Animator.Play("attack1");
                }
            }
        }
    }
}