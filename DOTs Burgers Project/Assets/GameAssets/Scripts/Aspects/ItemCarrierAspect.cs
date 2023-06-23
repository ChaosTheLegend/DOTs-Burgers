using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GameAssets.Scripts.Temp
{
    public readonly partial struct ItemCarrierAspect : IAspect
    {
        private readonly Entity _entity;
        
        private readonly RefRW<ItemCarryComponent> _itemCarryComponent;
        private readonly RefRO<LocalTransform> _localTransform;
        
        public void DropItem()
        {
            _itemCarryComponent.ValueRW.Item = ItemData.Null;
        }
        
        public void PickUpItem(ItemData item)
        {
            _itemCarryComponent.ValueRW.Item = item;
        }
        
        public ItemData GetItem()
        {
            return _itemCarryComponent.ValueRO.Item;
        }
        
        public float3 GetPosition()
        {
            return _localTransform.ValueRO.Position;
        }
    }
}