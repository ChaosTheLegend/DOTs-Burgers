using GameAssets.Scripts.Components;
using GameAssets.Scripts.Temp;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GameAssets.Scripts.Aspects
{
    public readonly partial struct InWorldItemPickupAspect : IAspect
    {
        private readonly Entity _entity;
        
        private readonly RefRW<ItemPickupComponent> _itemPickupComponent;
        private readonly RefRO<LocalTransform> _localTransform;
        private readonly RefRO<ItemInWorldTagComponent> _itemInWorldTagComponent;
        
        //exclude isPickedUpTagComponent
        
        
        
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
        
        public Entity GetEntity()
        {
            return _entity;
        }
    }
}