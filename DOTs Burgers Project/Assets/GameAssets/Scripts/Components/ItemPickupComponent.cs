using Unity.Entities;

namespace GameAssets.Scripts.Temp
{
    public struct ItemPickupComponent : IComponentData
    {
        public ItemData Item;
    }
}