using EntityComponents;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    /// <summary>
    /// Used to contain the random logic that randomly positions the grass based on a seed that can be set.
    /// </summary>
    public readonly partial struct GenerationAspect : IAspect
    {
        public readonly RefRW<RandomComponent> random;
        public readonly RefRW<MapComponent> map;

        public LocalTransform GetRandomTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = quaternion.identity,
                Scale = 0.01f
            };
        }

        private float3 GetRandomPosition()
        {
            return random.ValueRW.value.NextFloat3(MinDimensions, MaxDimensions);
        }

        private float3 MinDimensions => new float3(0, 0, 0);
        private float3 MaxDimensions => new float3(map.ValueRO.worldDimensions.x, 0, map.ValueRO.worldDimensions.y);
    }
}