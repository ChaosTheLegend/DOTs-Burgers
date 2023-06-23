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
                AddComponent(entity, new ItemPickupComponent { Item = authoring.Item });
            }
        }
    }
}