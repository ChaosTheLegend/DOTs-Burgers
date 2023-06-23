using System;
using GameAssets.Scripts.Components;
using Rewired;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace GameAssets.Scripts.Authoring
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField]
        private int playerId = 0;
        [Space]
        [SerializeField]
        private float playerSpeed = 1f;

        
        public class PlayerTagComponentBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerTagComponent { AssignedPlayerId = authoring.playerId });
                AddComponent(entity, new SpeedComponent { Value = authoring.playerSpeed });
                AddComponent(entity, new DirectionComponent { Value = float3.zero });
                AddComponent(entity, new PlayerInputComponent());
            }
        }
    }
}