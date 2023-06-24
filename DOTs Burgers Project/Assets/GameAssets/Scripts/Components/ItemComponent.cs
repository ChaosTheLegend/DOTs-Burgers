using GameAssets.Scripts.Temp;
using Unity.Entities;

namespace GameAssets.Scripts.Components
{
    public struct ItemComponent : IComponentData
    {
        public ItemData Item;
    }
}