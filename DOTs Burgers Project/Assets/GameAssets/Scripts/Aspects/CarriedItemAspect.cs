﻿using GameAssets.Scripts.Components;
using GameAssets.Scripts.Temp;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GameAssets.Scripts.Aspects
{
    public readonly partial struct CarriedItemAspect : IAspect
    {
        private readonly Entity _entity;
        
        private readonly RefRW<ItemComponent> _itemPickupComponent;
        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRO<IsPickedUpTagComponent> _isPickedUpTagComponent;
        private readonly RefRW<Parent> _parent;
        
        public void ResetParentIfIncorrect()
        {
            if(_parent.ValueRO.Value == _isPickedUpTagComponent.ValueRO.pickedUpBy) return;
            _parent.ValueRW.Value = _isPickedUpTagComponent.ValueRO.pickedUpBy;
        }
        
        public void GlueToParent()
        {
            if(math.lengthsq(_localTransform.ValueRO.Position - _isPickedUpTagComponent.ValueRO.positionOffset) == 0f) return;
            _localTransform.ValueRW.Position = _isPickedUpTagComponent.ValueRO.positionOffset;
        }
        
        public ItemData GetItem()
        {
            return _itemPickupComponent.ValueRO.Item;
        }
    }
}