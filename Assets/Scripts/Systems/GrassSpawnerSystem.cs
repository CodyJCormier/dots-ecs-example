using Aspects;
using EntityComponents;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    [BurstCompile]
    public partial struct GrassSpawnerSystems : ISystem
    {
        // how long and wide we add grass, this is fetched from the map for it's size.
        private float _width;
        private float _length;
        /// <summary>
        /// the pLimit we use to create patterns that we use when finding a position to place the grass.
        /// </summary>
        private float _perlinLimit;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _perlinLimit = 0.80f;

            state.RequireForUpdate<GrassSpawnerComponent>();
            state.RequireForUpdate<MapComponent>();
            state.RequireForUpdate<RandomComponent>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // Collect needed values
            var grassSpawnerComponent = SystemAPI.GetSingleton<GrassSpawnerComponent>();
            var mapComponentEntity = SystemAPI.GetSingletonEntity<MapComponent>();

            // Get the Aspect that we have generation methods in
            var generationAspect = SystemAPI.GetAspectRW<GenerationAspect>(mapComponentEntity);

            // Get Entity buffer to spawn grass
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            var grassToSpawn = generationAspect.map.ValueRO.grassObjectsToSpawn;

            for (int i = 0; i < grassToSpawn; i++)
            {
                LocalTransform transform = generationAspect.GetRandomTransform();

                int j = 0;
                while (Mathf.PerlinNoise(transform.Position.x, transform.Position.z) < _perlinLimit)
                {
                    transform = generationAspect.GetRandomTransform();
                    j++;
                    // break to prevent infinite loop in case a spot truly cannot be found.
                    if (j > 200) break;
                }

                var spawnedTile = entityCommandBuffer.Instantiate(grassSpawnerComponent.grassEntity);
                entityCommandBuffer.SetComponent(spawnedTile, transform);
            }

            entityCommandBuffer.Playback(state.EntityManager);
            state.Enabled = false;
        }
    }
}