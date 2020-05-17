using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;



public class WaveBaseSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float elapsedTime = (float) Time.ElapsedTime;
        
        Entities.ForEach((ref Translation trans, ref MoveSpeed moveSpeed, ref WaveData waveData) =>
        {
            float zPosition = waveData.amplitude * math.sin( elapsedTime * moveSpeed.Value
                                                             + trans.Value.x * waveData.xOffset
                                                             + trans.Value.y * waveData.yOffeet);


            trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
        }).Schedule();
    }
}

/*
[BurstCompile]
public class WaveJobSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float elapsedTime = (float) Time.ElapsedTime;
        
        var jobHandle = Entities.ForEach((ref Translation trans, ref MoveSpeed moveSpeed, ref WaveData waveData) =>
        {
            float zPosition = waveData.amplitude * math.sin( elapsedTime * moveSpeed.Value
                                                             + trans.Value.x * waveData.xOffset
                                                             + trans.Value.y * waveData.yOffeet);


            trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
        }).Schedule(inputDeps);

        return jobHandle;
    }
}*/

/*public class WaveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation trans, ref MoveSpeed moveSpeed, ref WaveData waveData) =>
        {
            float zPosition = waveData.amplitude * math.sin((float)Time.ElapsedTime * moveSpeed.Value
                              + trans.Value.x * waveData.xOffset 
                              + trans.Value.y * waveData.yOffeet);
            
            
            trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
        });
    }
}*/