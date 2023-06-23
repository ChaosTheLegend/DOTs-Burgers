using GameAssets.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;

namespace GameAssets.Scripts.Aspects
{
    public readonly partial struct PlayerInputAspect : IAspect
    {
        private readonly Entity _entity;
        
        private readonly RefRO<PlayerTagComponent> _playerTagComponent;
        public readonly RefRW<PlayerInputComponent> playerInputComponent;
        
        public int GetPlayerId()
        {
            return _playerTagComponent.ValueRO.AssignedPlayerId;
        }
    }
}