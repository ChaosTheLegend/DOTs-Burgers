using Unity.Entities;
using Unity.Mathematics;

namespace GameAssets.Scripts.Components
{
    public struct PlayerInputComponent : IComponentData
    {
        public float2 movementAxis;

        public bool pickUp;
        
        public bool drop;
    }
}