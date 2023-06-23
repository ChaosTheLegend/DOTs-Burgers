using GameAssets.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;

namespace GameAssets.Scripts.Aspects
{
    public readonly partial struct PlayerMovementAspect : IAspect
    {
        private readonly Entity _entity;
        
        private readonly RefRW<DirectionComponent> _directionComponent;
        private readonly RefRO<PlayerInputComponent> _playerInputComponent;
        
        public void ProcessMovement()
        {
            var movementAxis = _playerInputComponent.ValueRO.movementAxis;
            var direction = new float3(movementAxis.x, 0, movementAxis.y);
            if (math.lengthsq(direction) > 1) direction = math.normalize(direction);
            _directionComponent.ValueRW.Value = direction;
        }
    }
}