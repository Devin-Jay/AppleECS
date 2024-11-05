using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

[UpdateAfter(typeof(TimerSystem))]
public partial struct AppleSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        var sceneName = SceneManager.GetActiveScene().name;
        var difficulty = 0;
        if (!sceneName.Contains("Easy"))
        {
            difficulty = 1;
        }

        new SpawnJob { ECB = ecb, Difficulty = difficulty}.Schedule();
    }

    [BurstCompile]
    private partial struct SpawnJob : IJobEntity
    {
        public EntityCommandBuffer ECB;
        public int Difficulty;

        private void Execute(ref AppleSpawnerRandom random, in LocalTransform transform, in AppleSpawner spawner, ref Timer timer)
        {
            if (timer.Value > 0)
                return;

            timer.Value = spawner.Interval;

            Entity appleEntity;

            if (Difficulty != 0)
            {
                float randomValue = random.Value.NextFloat();

                if (randomValue < 0.5f)
                {
                    appleEntity = ECB.Instantiate(spawner.Prefab);
                }
                else
                {
                    appleEntity = ECB.Instantiate(spawner.BadPrefab);
                }
            }
            else
            {
                appleEntity = ECB.Instantiate(spawner.Prefab);
            }
            
            
            ECB.SetComponent(appleEntity, LocalTransform.FromPosition(transform.Position));
        }
    }
}
