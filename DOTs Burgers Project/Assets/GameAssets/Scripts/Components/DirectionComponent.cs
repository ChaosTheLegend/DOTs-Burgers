using Unity.Entities;
using Unity.Mathematics;

namespace GameAssets.Scripts.Aspects
{
    public partial struct DirectionComponent : IComponentData
    {
        public float3 Value;
    }
}