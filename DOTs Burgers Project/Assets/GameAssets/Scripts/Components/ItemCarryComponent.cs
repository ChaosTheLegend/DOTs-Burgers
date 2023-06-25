using Unity.Entities;
using Unity.Mathematics;

namespace GameAssets.Scripts.Temp
{
    public struct ItemCarryComponent : IComponentData
    {
        public ItemData Item;
        
        public Entity ItemEntity;
        
        public float3 itemPositionOffset;
    }
}