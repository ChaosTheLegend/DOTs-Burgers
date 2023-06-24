using GameAssets.Scripts.Components;
using GameAssets.Scripts.Temp;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace GameAssets.Scripts.Systems
{


    public partial struct ItemPickupDestroyJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ParallelWriter;

        [BurstCompile]
        private void Execute(ItemPickupDestroyAspect itemPickup)
        {
            if(itemPickup.ItemPickupComponent.ValueRO.Item.Equals(ItemData.Null))
                ParallelWriter.AddComponent<DestroyedTagComponent>(0, itemPickup.entity);
        }
        
    }
    
    public readonly partial struct ItemPickupDestroyAspect : IAspect
    {
        public readonly Entity entity;
        
        public readonly RefRO<ItemPickupComponent> ItemPickupComponent;
    }
    
    public partial struct ItemPickupDestroySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);
            var parallelWriter = entityCommandBuffer.AsParallelWriter();
            
            var job = new ItemPickupDestroyJob
            {
                ParallelWriter = parallelWriter
            };

            job.ScheduleParallel(state.Dependency).Complete();
            
            entityCommandBuffer.Playback(state.EntityManager);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}