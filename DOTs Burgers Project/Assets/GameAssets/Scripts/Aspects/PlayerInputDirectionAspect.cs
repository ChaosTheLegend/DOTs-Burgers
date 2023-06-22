using Unity.Entities;
using Unity.Mathematics;

namespace GameAssets.Scripts.Aspects
{
    public readonly partial struct PlayerInputDirectionAspect : IAspect
    {
        public readonly Entity Entity;
        
        private readonly RefRO<PlayerTagComponent> _playerTagComponent;
        private readonly RefRW<DirectionComponent> _directionComponent;
        
        public int GetPlayerId()
        {
            return _playerTagComponent.ValueRO.AssignedPlayerId;
        }
        
        public void SetDirection(float3 direction)
        {
            _directionComponent.ValueRW.Value = direction;
        }
    }
}