using GameAssets.Scripts.Aspects;
using Rewired;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace GameAssets.Scripts.Systems
{
    public partial class PlayerInputProcessSystem : SystemBase
    {
        
        //This cannot be burst compiled because it uses Rewired
        
        protected override void OnUpdate()
        {
            foreach (var entity in SystemAPI.Query<PlayerInputAspect>())
            {
                var player = ReInput.players.GetPlayer(entity.GetPlayerId());
                var horizontal = player.GetAxis("MoveHorizontal");
                var vertical = player.GetAxis("MoveVertical");
                
                var movementAxis = new float2(horizontal,vertical);
                
                var pickUp = player.GetButtonDown("PickUp");
                
                entity.playerInputComponent.ValueRW.pickUp = pickUp;
                entity.playerInputComponent.ValueRW.movementAxis = movementAxis;
            }
        }
    }
}