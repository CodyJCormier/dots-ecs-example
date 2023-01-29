using Unity.Entities;

namespace EntityComponents
{
    public struct TileSpawnerComponent : IComponentData
    {
        public Entity tilePrefab;
    }
}