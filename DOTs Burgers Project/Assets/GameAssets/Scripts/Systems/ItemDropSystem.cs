using GameAssets.Scripts.Components;
using GameAssets.Scripts.Temp;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace GameAssets.Scripts.Systems
{
    public partial struct ItemDropSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var carrier in SystemAPI.Query<ItemCarrierAspect>())
            {
                if(!carrier.IsDropPressed()) continue;
                
                if (carrier.GetItem().Equals(ItemData.Null)) continue;

                var item = carrier.GetPickedUpEntity();
                
                if (item.Equals(Entity.Null)) continue;
                
                
                entityCommandBuffer.RemoveComponent<IsPickedUpTagComponent>(item);
                
                entityCommandBuffer.AddComponent<ItemInWorldTagComponent>(item);
                
                entityCommandBuffer.RemoveComponent<Parent>(item);
                
                var childPosition = state.EntityManager.GetComponentData<LocalTransform>(item);
                childPosition.Position += carrier.GetPosition();
                entityCommandBuffer.SetComponent(item, childPosition);
                
                carrier.DropItem();
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}