using GameAssets.Scripts.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace GameAssets.Scripts.Systems
{
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
            
            foreach (var entity in SystemAPI.Query<EntityMovementAspect>())
            {
                entity.MoveTowardsDirection(deltaTime);
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}