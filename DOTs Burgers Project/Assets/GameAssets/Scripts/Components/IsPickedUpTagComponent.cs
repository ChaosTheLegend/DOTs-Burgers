using Unity.Entities;
using Unity.Mathematics;

namespace GameAssets.Scripts.Components
{
    public struct IsPickedUpTagComponent : IComponentData
    {
        public Entity pickedUpBy;
        
        public float3 positionOffset;
    }
}