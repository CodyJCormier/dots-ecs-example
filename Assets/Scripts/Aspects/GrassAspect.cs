using EntityTags;
using Unity.Entities;
using Unity.Transforms;

namespace Aspects
{
    /// <summary>
    /// Used to fetch the grass that has a TransformAspect and a GrassTagComponent
    /// </summary>
    public readonly partial struct GrassAspect : IAspect
    {
        private readonly RefRW<GrassTagComponent> _growth;
        private readonly TransformAspect _transformAspect;

        /// <summary>
        /// Grows the grass by updating the growth value, and increasing the in game scale.
        /// </summary>
        public void Grow()
        {
            _growth.ValueRW.growthAmount += 0.01f;
            _transformAspect.LocalScale = 1 * _growth.ValueRO.growthAmount;
        }

        public bool FullyGrown()
        {
            return _growth.ValueRO.growthAmount >= 1f;
        }
    }
}
