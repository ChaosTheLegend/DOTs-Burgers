using Unity.Entities;
using Unity.Mathematics;

namespace GameAssets.Scripts.Components
{
    public partial struct DirectionComponent : IComponentData
    {
        public float3 Value;
    }
}