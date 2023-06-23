using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace GameAssets.Scripts.Temp
{
    public partial struct ItemPickupSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var carrier in SystemAPI.Query<ItemCarrierAspect>())
            {
                if (!carrier.GetItem().Equals(ItemData.Null)) continue;
            
                foreach (var pickup in SystemAPI.Query<ItemPickupAspect>())
                {
                    if (pickup.GetItem().Equals(ItemData.Null)) continue;
            
                    if (math.distancesq(carrier.GetPosition(), pickup.GetPosition()) < 1f)
                    {
                        carrier.PickUpItem(pickup.GetItem());
                        pickup.DeleteItem();
                    }
                }
            }
        }
        
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
    
}