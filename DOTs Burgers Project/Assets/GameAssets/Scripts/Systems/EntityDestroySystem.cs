using GameAssets.Scripts.Aspects;
using GameAssets.Scripts.Components;
using GameAssets.Scripts.Temp;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace GameAssets.Scripts.Systems
{
    public partial struct EntityDestroyJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter EntityCommandBuffer;

        [BurstCompile]
        private void Execute(DestroyTagAspect destroyTag)
        {
            EntityCommandBuffer.DestroyEntity(0, destroyTag.entity);
        }
    }
    
    public readonly partial struct DestroyTagAspect : IAspect
    {
        public readonly Entity entity;
        
        public readonly RefRO<DestroyedTagComponent> ValueRO;
    }
    
    public partial struct EntityDestroySystem : ISystem
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
            
            var job = new EntityDestroyJob
            {
                EntityCommandBuffer = parallelWriter
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