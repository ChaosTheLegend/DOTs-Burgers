using GameAssets.Scripts.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace GameAssets.Scripts.Systems
{
    [BurstCompile]
    public partial struct PlayerMoveJob : IJobEntity
    {
        [BurstCompile]
        public void Execute(PlayerMovementAspect entity)
        {
            entity.ProcessMovement();
        }
    }
    public partial struct PlayerMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //do a job
            var jobHandle = new PlayerMoveJob().ScheduleParallel(state.Dependency);
            
            jobHandle.Complete();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
    
    
}