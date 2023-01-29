using EntityTags;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class TileTagAuthoring : MonoBehaviour
    {

    }

    public class TileTagBaker : Baker<TileTagAuthoring>
    {
        public override void Bake(TileTagAuthoring authoring)
        {
            AddComponent(new TileTagComponent
            {

            });
        }
    }
}