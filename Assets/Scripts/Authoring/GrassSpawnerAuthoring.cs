using EntityComponents;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class GrassSpawnerAuthoring : MonoBehaviour
    {
        public GameObject grass;
    }

    public class GrassSpawnerBaker : Baker<GrassSpawnerAuthoring>
    {
        public override void Bake(GrassSpawnerAuthoring authoring)
        {
            AddComponent(new GrassSpawnerComponent
            {
                grassEntity = GetEntity(authoring.grass)
            });
        }
    }
}