using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GameAssets.Scripts.Aspects
{
    public readonly partial struct EntityMovementAspect : IAspect
    {
        public readonly Entity Entity;
        
        private readonly RefRO<SpeedComponent> _speedComponent;
        private readonly RefRW<DirectionComponent> _directionComponent;
        private readonly RefRW<LocalTransform> _localTransform;
        
        //make a function to move the player
        public void MoveTowardsDirection(float deltaTime)
        {
            _localTransform.ValueRW.Position += _directionComponent.ValueRO.Value * _speedComponent.ValueRO.Value * deltaTime;
        }
        
        public float3 GetPosition()
        {
            return _localTransform.ValueRO.Position;
        }
    }
}