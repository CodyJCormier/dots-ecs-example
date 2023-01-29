using EntityComponents;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    [BurstCompile]
    public partial struct TileSpawnerSystems : ISystem
    {
        private int _tileCountX;
        private int _tileCountY;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _tileCountX = 500;
            _tileCountY = 500;
            state.RequireForUpdate<TileSpawnerComponent>();
            state.RequireForUpdate<MapComponent>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var tileSpawnerComponent = SystemAPI.GetSingleton<TileSpawnerComponent>();
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            var mapComponent = SystemAPI.GetSingleton<MapComponent>();
            _tileCountX = mapComponent.worldDimensions.x;
            _tileCountY = mapComponent.worldDimensions.y;


            for (int i = 0; i < _tileCountX; i++)
            {
                for (int j = 0; j < _tileCountY; j++)
                {
                    // Spawn the tiles
                    Entity spawnedTile = entityCommandBuffer.Instantiate(tileSpawnerComponent.tilePrefab);
                    entityCommandBuffer.SetComponent(spawnedTile, new LocalTransform
                    {
                        // The position of the quad in the array
                        Position = new float3(i, 0, j),
                        // The rotation for the quad so that it's visual side is face up.
                        Rotation = Quaternion.Euler(90, 0, 0),
                        Scale = 1f
                    });
                }
            }

            entityCommandBuffer.Playback(state.EntityManager);

            state.Enabled = false;
        }
    }
}
