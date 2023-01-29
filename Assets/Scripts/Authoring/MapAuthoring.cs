using EntityComponents;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Authoring
{
    public class MapTagAuthoring : MonoBehaviour
    {
        public int2 worldDimensions;
        public float grassObjectsToSpawn;
        public uint randomSeed;
    }

    public class MapTagBaker : Baker<MapTagAuthoring>
    {
        public override void Bake(MapTagAuthoring authoring)
        {
            AddComponent(new MapComponent
            {
                 worldDimensions = authoring.worldDimensions,
                 grassObjectsToSpawn = authoring.grassObjectsToSpawn
            });
            AddComponent(new RandomComponent
            {
                value = Random.CreateFromIndex(authoring.randomSeed)
            });
        }
    }
}