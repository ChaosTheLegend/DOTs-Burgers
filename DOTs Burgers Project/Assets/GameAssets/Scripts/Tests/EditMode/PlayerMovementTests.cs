using System.Collections;
using GameAssets.Scripts.Components;
using GameAssets.Scripts.Systems;
using NUnit.Framework;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor;
using UnityEngine.TestTools;

namespace GameAssets.Scripts.Tests.EditMode
{
    public class PlayerMovementTests : ECSTestBase
    {

        [Test]
        public void DirectionZeroWhenNoInput()
        {
            var player = EntityManager.CreateEntity(
                typeof(DirectionComponent),
                typeof(PlayerInputComponent)
                );
            
            EntityManager.SetComponentData(player, new DirectionComponent { Value = new float3(1, 1, 1) });
            EntityManager.SetComponentData(player, new PlayerInputComponent { movementAxis = new float2(0, 0) });

            World.CreateSystem<PlayerMovementSystem>();
            World.Update();
            
            Assert.AreEqual(float3.zero, EntityManager.GetComponentData<DirectionComponent>(player).Value);
        }
        
        [Test]
        public void MoveForwardWhenInput()
        {
            var player = EntityManager.CreateEntity(
                typeof(DirectionComponent),
                typeof(PlayerInputComponent)
            );
            
            EntityManager.SetComponentData(player, new DirectionComponent { Value = new float3(0, 0, 0) });
            EntityManager.SetComponentData(player, new PlayerInputComponent { movementAxis = new float2(0, 1) });

            World.CreateSystem<PlayerMovementSystem>();
            World.Update();
            
            Assert.AreEqual(new float3(0f,0f,1f), EntityManager.GetComponentData<DirectionComponent>(player).Value);
        }
        
        [Test]
        public void MoveBackwardWhenInput()
        {
            var player = EntityManager.CreateEntity(
                typeof(DirectionComponent),
                typeof(PlayerInputComponent)
            );
            
            EntityManager.SetComponentData(player, new DirectionComponent { Value = new float3(0, 0, 0) });
            EntityManager.SetComponentData(player, new PlayerInputComponent { movementAxis = new float2(0, -1) });

            World.CreateSystem<PlayerMovementSystem>();
            World.Update();
            
            Assert.AreEqual(new float3(0f,0f,-1f), EntityManager.GetComponentData<DirectionComponent>(player).Value);
        }
        
        [Test]
        public void MoveLeftWhenInput()
        {
            var player = EntityManager.CreateEntity(
                typeof(DirectionComponent),
                typeof(PlayerInputComponent)
            );
            
            EntityManager.SetComponentData(player, new DirectionComponent { Value = new float3(0, 0, 0) });
            EntityManager.SetComponentData(player, new PlayerInputComponent { movementAxis = new float2(1, 0) });

            World.CreateSystem<PlayerMovementSystem>();
            World.Update();
            
            Assert.AreEqual(new float3(1f,0f,0f), EntityManager.GetComponentData<DirectionComponent>(player).Value);
        }
        
        [Test]
        public void MoveRightWhenInput()
        {
            var player = EntityManager.CreateEntity(
                typeof(DirectionComponent),
                typeof(PlayerInputComponent)
            );
            
            EntityManager.SetComponentData(player, new DirectionComponent { Value = new float3(0, 0, 0) });
            EntityManager.SetComponentData(player, new PlayerInputComponent { movementAxis = new float2(-1, 0) });

            World.CreateSystem<PlayerMovementSystem>();
            World.Update();
            
            Assert.AreEqual(new float3(-1f,0f,0f), EntityManager.GetComponentData<DirectionComponent>(player).Value);
        }

        [Test]
        public void MoveNormalizedWhenMultipleInputs()
        {
            var player = EntityManager.CreateEntity(
                typeof(DirectionComponent),
                typeof(PlayerInputComponent)
            );
            
            EntityManager.SetComponentData(player, new DirectionComponent { Value = new float3(0, 0, 0) });
            EntityManager.SetComponentData(player, new PlayerInputComponent { movementAxis = new float2(1, 1) });

            World.CreateSystem<PlayerMovementSystem>();
            World.Update();
            
            var expected = math.normalize(new float3(1f,0f,1f));
            
            Assert.AreEqual(expected, EntityManager.GetComponentData<DirectionComponent>(player).Value);
        }
    }
}