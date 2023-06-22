using GameAssets.Scripts.Aspects;
using Rewired;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace GameAssets.Scripts.Systems
{
    public partial class PlayerInputProcessSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            foreach (var entity in SystemAPI.Query<PlayerInputDirectionAspect>())
            {
                var player = ReInput.players.GetPlayer(entity.GetPlayerId());
                var horizontal = player.GetAxis("MoveHorizontal");
                var vertical = player.GetAxis("MoveVertical");
                
                var direction = new float3(horizontal, 0, vertical);

                //Fix for NANf error
                if (math.lengthsq(direction) > 1f) direction = math.normalize(direction);
                
                entity.SetDirection(direction);
            }
        }
    }
}