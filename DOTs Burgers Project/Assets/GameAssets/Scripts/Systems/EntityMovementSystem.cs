using GameAssets.Scripts.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace GameAssets.Scripts.Systems
{
    public partial struct EntityMovementJob : IJobEntity
    {
        public float deltaTime;
        [BurstCompile]
        private void Execute(EntityMovementAspect entity)
        {
            entity.MoveTowardsDirection(deltaTime);
        }
    }
    
    public partial struct EntityMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = state.World.Time.DeltaTime;
            
            var jobHandle = new EntityMovementJob
            {
                deltaTime = deltaTime
            }.ScheduleParallel(state.Dependency);
            
            jobHandle.Complete();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}