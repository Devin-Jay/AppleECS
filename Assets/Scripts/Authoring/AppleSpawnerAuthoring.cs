using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = Unity.Mathematics.Random;

public struct AppleSpawner : IComponentData
{
    public Entity Prefab;
    public Entity BadPrefab;
    public float Interval;
}

public struct AppleSpawnerRandom : IComponentData
{
    public Random Value;
}

[DisallowMultipleComponent]
public class AppleSpawnerAuthoring : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject badApplePrefab;
    [SerializeField] private float appleSpawnInterval = 1f;

    private class AppleSpawnerAuthoringBaker : Baker<AppleSpawnerAuthoring>
    {
        public override void Bake(AppleSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new AppleSpawner
            {
                Prefab = GetEntity(authoring.applePrefab, TransformUsageFlags.Dynamic),
                BadPrefab = GetEntity(authoring.badApplePrefab, TransformUsageFlags.Dynamic),
                Interval = authoring.appleSpawnInterval,
            });
            AddComponent(entity, new AppleSpawnerRandom
            {
                Value = Random.CreateFromIndex((uint)entity.Index)
            });
            AddComponent(entity, new Timer { Value = 2f });
        }
    }
}
