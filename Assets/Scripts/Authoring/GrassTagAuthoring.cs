using EntityTags;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class GrassTagAuthoring : MonoBehaviour
    {
        [Tooltip("starts at 0.01f, and grows to 1f (100%)")]
        public float growthAmount;
    }

    public class GrassTagBaker : Baker<GrassTagAuthoring>
    {
        public override void Bake(GrassTagAuthoring authoring)
        {
            AddComponent(new GrassTagComponent
            {
                growthAmount = authoring.growthAmount
            });
        }
    }
}