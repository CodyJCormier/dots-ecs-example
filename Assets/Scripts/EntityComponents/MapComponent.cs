using Unity.Entities;
using Unity.Mathematics;

namespace EntityComponents
{
    public struct MapComponent : IComponentData
    {
        public int2 worldDimensions;
        public float grassObjectsToSpawn;
    }
}