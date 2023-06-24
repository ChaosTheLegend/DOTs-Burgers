using GameAssets.Scripts.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace GameAssets.Scripts.Temp
{
    
  
    public partial struct PickUpJob : IJobEntity
    {
        [BurstCompile]
        public void Execute(ItemCarrierAspect carrier, Aspects.InWorldItemPickupAspect pickup)
        {
            if (!carrier.IsPickUpPressed()) return;
            if (!carrier.GetItem().Equals(ItemData.Null)) return;
            if (pickup.GetItem().Equals(ItemData.Null)) return;
        
            if (math.distancesq(carrier.GetPosition(), pickup.GetPosition()) < 1f)
            {
                carrier.PickUpItem(pickup.GetItem());
                pickup.DeleteItem();
            }
        }
    }
    public partial struct ItemPickupSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        
        //This can be moved to a job but I don't know how to do pass 2 queries at once
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var carrier in SystemAPI.Query<ItemCarrierAspect>())
            {
                if (!carrier.IsPickUpPressed()) continue;
                if (!carrier.GetItem().Equals(ItemData.Null)) continue;
                
            
                foreach (var pickup in SystemAPI.Query<Aspects.InWorldItemPickupAspect>())
                {
                    if (pickup.GetItem().Equals(ItemData.Null)) continue;
            
                    if (math.distancesq(carrier.GetPosition(), pickup.GetPosition()) < 1f)
                    {
                        carrier.PickUpItem(pickup.GetItem());
                        entityCommandBuffer.AddComponent<IsPickedUpTagComponent>(pickup.GetEntity());
                        entityCommandBuffer.AddComponent<Parent>(pickup.GetEntity());
                        entityCommandBuffer.RemoveComponent<ItemInWorldTagComponent>(pickup.GetEntity());
                        entityCommandBuffer.SetComponent(pickup.GetEntity(), new IsPickedUpTagComponent()
                        {
                            pickedUpBy = carrier.GetEntity(),
                            positionOffset = carrier.GetItemPositionOffset()
                        });
                        entityCommandBuffer.SetComponent(pickup.GetEntity(), new Parent()
                        {
                            Value = carrier.GetEntity()
                        });
                    }
                }
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            
            
        }
        
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
    
}