using GameAssets.Scripts.Components;
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
        private readonly RefRO<PlayerInputComponent> _playerInputComponent;
        public void DropItem()
        {
            _itemCarryComponent.ValueRW.Item = ItemData.Null;
            _itemCarryComponent.ValueRW.ItemEntity = Entity.Null;
        }
        
        public float3 GetItemPositionOffset()
        {
            return _itemCarryComponent.ValueRO.itemPositionOffset;
        }
        
        public bool IsPickUpPressed()
        {
            return _playerInputComponent.ValueRO.pickUp;
        }

        public bool IsDropPressed()
        {
            return _playerInputComponent.ValueRO.drop;
        }
        
        public void PickUpItem(ItemData item, Entity entity)
        {
            _itemCarryComponent.ValueRW.Item = item;
            _itemCarryComponent.ValueRW.ItemEntity = entity;
        }
        
        public ItemData GetItem()
        {
            return _itemCarryComponent.ValueRO.Item;
        }
        
        public float3 GetPosition()
        {
            return _localTransform.ValueRO.Position;
        }
        
        public Entity GetEntity()
        {
            return _entity;
        }

        public Entity GetPickedUpEntity()
        {
            return _itemCarryComponent.ValueRO.ItemEntity;
        }
    }
}