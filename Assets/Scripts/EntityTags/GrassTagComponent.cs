using Unity.Entities;

namespace EntityTags
{
    public struct GrassTagComponent : IComponentData
    {
        /// <summary>
        /// starts at 0.01f, and grows to 1f (100%)
        /// </summary>
        public float growthAmount;
    }
}
