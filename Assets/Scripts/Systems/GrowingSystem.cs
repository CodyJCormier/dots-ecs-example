using Aspects;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    /// <summary>
    /// Iterates over all the grass entities and 'grows' them by ticking them. In turn grass grows 1% per tick performed at _timeToUpdate intervals.
    /// </summary>
    [BurstCompile]
    public partial struct GrowingSystem : ISystem
    {
        /// <summary>
        /// The current time between updates left, after reaching _timeToUpdate value, will perform a tick and begin again
        /// </summary>
        private float _updateTime;
        /// <summary>
        /// How much time should elapse between ticks to trigger growth.
        /// </summary>
        private float _timeToUpdate;
        [BurstCompile]

        public void OnCreate(ref SystemState state)
        {
            _updateTime = 0;
            _timeToUpdate = 0.1f;
        }
        [BurstCompile]

        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            _updateTime += SystemAPI.Time.DeltaTime;

            // If we have not hit an update time yet, stop.
            if (_updateTime < _timeToUpdate)
                return;

            // Subtract the time to growth update
            _updateTime -= _timeToUpdate;

            // Perform the updates to the grass to trigger growth.
            foreach (var grass in SystemAPI.Query<GrassAspect>())
            {
                // If the grass is already full grown, we can check the next grass.
                // Ideally, removing the grass that does not need to be grown from this iterator
                if (grass.FullyGrown()) continue;

                grass.Grow();
            }
        }
    }
}
