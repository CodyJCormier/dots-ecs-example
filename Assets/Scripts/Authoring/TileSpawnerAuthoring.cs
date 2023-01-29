using EntityComponents;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class TileSpawnerAuthoring : MonoBehaviour
    {
        public GameObject tile;
    }

    public class TileSpawnerBaker : Baker<TileSpawnerAuthoring>
    {
        public override void Bake(TileSpawnerAuthoring authoring)
        {
            AddComponent(new TileSpawnerComponent
            {
                tilePrefab = GetEntity(authoring.tile)
            });
        }
    }
}