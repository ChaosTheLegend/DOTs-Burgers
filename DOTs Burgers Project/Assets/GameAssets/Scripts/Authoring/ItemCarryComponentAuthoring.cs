using Unity.Entities;
using UnityEngine;

namespace GameAssets.Scripts.Temp
{
    public class ItemCarryComponentAuthoring : MonoBehaviour
    {
        public ItemData Item = ItemData.Null;

        public class ItemCarryComponentBaker : Baker<ItemCarryComponentAuthoring>
        {
            public override void Bake(ItemCarryComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new ItemCarryComponent { Item = authoring.Item });
            }
        }
    }
}