using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Match3
{
    /*public class JewelCreateSystem : ComponentSystem
    {
        private float spawnTime;
        private Random random;

        protected override void OnCreate()
        {
            base.OnCreate();
            random = new Random(55);

        }

        protected override void OnUpdate()
        {
            spawnTime -= Time.DeltaTime;
            
            if (spawnTime > 0)
                return;

            Entity spawned = EntityManager.Instantiate(JewelStaticPrefabs.heavy);
            EntityManager.SetComponentData(spawned, new Translation
            {
                Value =  new float3(random.NextFloat3(new float3(10, 10 ,10) - new float3(5, 5 ,5)))
            });
            
            /*Entities.ForEach((ref JewelPrefabs prefabs) =>
            {
                Entity spawned = EntityManager.Instantiate(prefabs.blue);
                EntityManager.SetComponentData(spawned, new Translation
                {
                    Value =  new float3(random.NextFloat3(new float3(5, 5 ,5)))
                });
            });* /
        }
    }*/
}