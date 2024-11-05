using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public struct BadAppleTag : IComponentData
{
}

public struct BadAppleSpeed : IComponentData
{
    public float Value;
}

public struct BadAppleBounds : IComponentData
{
    public float Left;
    public float Right;
}

public struct BadAppleDirectionChangeChance : IComponentData
{
    public float Value;
}

public struct BadAppleRandom : IComponentData
{
    public Random Value;
}

public class BadAppleAuthoring : MonoBehaviour
{
    [SerializeField] private float bottomY = -14f;
    [SerializeField] private float speed = 0.01f;
    [SerializeField] private float leftAndRightEdge = 24f;
    [SerializeField] private float directionChangeChance = 0.05f;
    private class BadAppleAuthoringBaker : Baker<BadAppleAuthoring>
    {
        public override void Bake(BadAppleAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<BadAppleTag>(entity);
            AddComponent(entity, new AppleBottomY { Value = authoring.bottomY });
            AddComponent(entity, new BadAppleSpeed { Value = authoring.speed });
            AddComponent(entity, new BadAppleBounds
            {
                Left = -authoring.leftAndRightEdge,
                Right = authoring.leftAndRightEdge
            });

            AddComponent(entity, new BadAppleDirectionChangeChance
            {
                Value = authoring.directionChangeChance
            });
            AddComponent(entity, new BadAppleRandom
            {
                Value = Random.CreateFromIndex((uint)entity.Index)
            });
        }
    }
}
