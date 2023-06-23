using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GameAssets.Scripts.Temp
{
    public readonly partial struct ItemPickupAspect : IAspect
    {
        private readonly Entity _entity;
        
        private readonly RefRW<ItemPickupComponent> _itemPickupComponent;
        private readonly RefRO<LocalTransform> _localTransform;
        
        public void DeleteItem()
        {
            _itemPickupComponent.ValueRW.Item = ItemData.Null;
        }
        
        public float3 GetPosition()
        {
            return _localTransform.ValueRO.Position;
        }
        
        public ItemData GetItem()
        {
            return _itemPickupComponent.ValueRO.Item;
        }
    }
}