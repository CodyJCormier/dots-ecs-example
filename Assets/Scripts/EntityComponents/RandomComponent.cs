using Unity.Entities;
using Unity.Mathematics;

namespace EntityComponents
{
    public struct RandomComponent : IComponentData
    {
        public Random value;
    }
}