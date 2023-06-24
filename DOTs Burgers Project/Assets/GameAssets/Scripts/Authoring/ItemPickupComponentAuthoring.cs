using GameAssets.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace GameAssets.Scripts.Temp
{
    public class ItemPickupComponentAuthoring : MonoBehaviour
    {
        public ItemData Item = ItemData.Null;

        public class ItemPickupComponentBaker : Baker<ItemPickupComponentAuthoring>
        {
            public override void Bake(ItemPickupComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new ItemComponent { Item = authoring.Item });
                AddComponent(entity, new ItemInWorldTagComponent());
            }
        }
    }
}