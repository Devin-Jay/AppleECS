using Unity.Burst;
using Unity.Entities;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct BadRandomDirectionChangeSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        new BadChangeDirectionJob().Schedule();
    }

    [BurstCompile]
    private partial struct BadChangeDirectionJob : IJobEntity
    {
        [BurstCompile]
        private void Execute(ref BadAppleRandom random, in BadAppleDirectionChangeChance chance,
            ref BadAppleSpeed speed)
        {
            if (random.Value.NextFloat() < chance.Value)
            {
                speed.Value *= -1;
            }
        }
    }
}
