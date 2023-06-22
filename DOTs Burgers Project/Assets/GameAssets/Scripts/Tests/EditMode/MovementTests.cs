using GameAssets.Scripts.Aspects;
using GameAssets.Scripts.Systems;
using NUnit.Framework;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace GameAssets.Scripts.Tests.EditMode
{
    [TestFixture]
    public class MovementTests
    {
    
        [Test]
        public void PlayerMovesForward()
        {
            var world = new World("TestWorld");
            var entityManager = world.EntityManager;
            
            var entity = entityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(SpeedComponent),
                typeof(DirectionComponent)
            );
            
            entityManager.SetComponentData(entity, new LocalTransform { Position = float3.zero });
            entityManager.SetComponentData(entity, new SpeedComponent { Value = 1f });
            entityManager.SetComponentData(entity, new DirectionComponent { Value = new float3(0, 0, 1) });

            world.CreateSystem<EntityMovementSystem>();
            world.Update();
            var deltaTime = world.Time.DeltaTime;
            
            // Assert
            Assert.AreEqual(entityManager.GetComponentData<LocalTransform>(entity).Position, new float3(0, 0, deltaTime));
        }
        
        //the same test but with a different speed
        [Test]
        public void PlayerMovesForwardWithSpeed5()
        {
            var world = new World("TestWorld");
            var entityManager = world.EntityManager;
            
            var entity = entityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(SpeedComponent),
                typeof(DirectionComponent)
            );
            
            entityManager.SetComponentData(entity, new LocalTransform { Position = float3.zero });
            entityManager.SetComponentData(entity, new SpeedComponent { Value = 5f });
            entityManager.SetComponentData(entity, new DirectionComponent { Value = new float3(0, 0, 1) });

            world.CreateSystem<EntityMovementSystem>();
            world.Update();
            var deltaTime = world.Time.DeltaTime;
            
            // Assert
            Assert.AreEqual(entityManager.GetComponentData<LocalTransform>(entity).Position, new float3(0, 0, deltaTime * 5));
        }
        
        //the same test but with a different direction
        [Test]
        public void PlayerMovesRight()
        {
            var world = new World("TestWorld");
            var entityManager = world.EntityManager;
            
            var entity = entityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(SpeedComponent),
                typeof(DirectionComponent)
            );
            
            entityManager.SetComponentData(entity, new LocalTransform { Position = float3.zero });
            entityManager.SetComponentData(entity, new SpeedComponent { Value = 1f });
            entityManager.SetComponentData(entity, new DirectionComponent { Value = new float3(1, 0, 0) });

            world.CreateSystem<EntityMovementSystem>();
            world.Update();
            var deltaTime = world.Time.DeltaTime;
            
            // Assert
            Assert.AreEqual(entityManager.GetComponentData<LocalTransform>(entity).Position, new float3(deltaTime, 0, 0));
        }
        
        //the same test but with a different starting position
        [Test]
        public void PlayerMovesForwardFromPosition1()
        {
            var world = new World("TestWorld");
            var entityManager = world.EntityManager;
            
            var entity = entityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(SpeedComponent),
                typeof(DirectionComponent)
            );
            
            entityManager.SetComponentData(entity, new LocalTransform { Position = new float3(0, 0, 1) });
            entityManager.SetComponentData(entity, new SpeedComponent { Value = 1f });
            entityManager.SetComponentData(entity, new DirectionComponent { Value = new float3(0, 0, 1) });

            world.CreateSystem<EntityMovementSystem>();
            world.Update();
            var deltaTime = world.Time.DeltaTime;
            
            // Assert
            Assert.AreEqual(entityManager.GetComponentData<LocalTransform>(entity).Position, new float3(0, 0, 1 + deltaTime));
        }

    }
}
