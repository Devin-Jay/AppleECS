
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.SceneManagement;
using UnityEngine;

public partial struct BadAppleMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var sceneName = SceneManager.GetActiveScene().name;
        int difficulty = 0;
        if (sceneName == "Hard")
        {
            Debug.Log(sceneName);
            difficulty = 2;
        }
        new MoveBadAppleJob { DeltaTime = deltaTime ,Difficulty = difficulty}.Schedule();
    }

    [BurstCompile]
    private partial struct MoveBadAppleJob : IJobEntity
    {
        public float DeltaTime;
        public int Difficulty;

        [BurstCompile]
        private void Execute(ref LocalTransform transform, ref BadAppleSpeed speed, in BadAppleBounds bounds, in BadAppleTag marker)
        {
            if (Difficulty == 2)
            {
                transform.Position.x += speed.Value * DeltaTime;

                if (transform.Position.x > bounds.Right)
                {
                    speed.Value = -math.abs(speed.Value);
                }
                else if (transform.Position.x < bounds.Left)
                {
                    speed.Value = math.abs(speed.Value);
                }
            }
            else
            {
                return;
            }
        }
    }
}
