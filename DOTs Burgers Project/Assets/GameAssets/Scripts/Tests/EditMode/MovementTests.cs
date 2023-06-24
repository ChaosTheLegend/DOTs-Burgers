using GameAssets.Scripts.Aspects;
using GameAssets.Scripts.Components;
using GameAssets.Scripts.Systems;
using NUnit.Framework;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GameAssets.Scripts.Tests.EditMode
{
    [TestFixture]
    public class MovementTests : ECSTestBase
    {
        [Test]
        public void PlayerMovesForward()
        {
            
            var entity = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(SpeedComponent),
                typeof(DirectionComponent)
            );
            
            EntityManager.SetComponentData(entity, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(entity, new SpeedComponent { Value = 1f });
            EntityManager.SetComponentData(entity, new DirectionComponent { Value = new float3(0, 0, 1) });

            World.CreateSystem<EntityMovementSystem>();
            World.Update();
            var deltaTime = World.Time.DeltaTime;
            
            // Assert
            Assert.AreEqual(EntityManager.GetComponentData<LocalTransform>(entity).Position, new float3(0, 0, deltaTime));
        }
        
        //the same test but with a different speed
        [Test]
        public void PlayerMovesForwardWithSpeed5()
        {
            var entity = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(SpeedComponent),
                typeof(DirectionComponent)
            );
            
            EntityManager.SetComponentData(entity, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(entity, new SpeedComponent { Value = 5f });
            EntityManager.SetComponentData(entity, new DirectionComponent { Value = new float3(0, 0, 1) });

            World.CreateSystem<EntityMovementSystem>();
            World.Update();
            var deltaTime = World.Time.DeltaTime;
            
            // Assert
            Assert.AreEqual(EntityManager.GetComponentData<LocalTransform>(entity).Position, new float3(0, 0, deltaTime * 5));
        }
        
        //the same test but with a different direction
        [Test]
        public void PlayerMovesRight()
        {
            var entity = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(SpeedComponent),
                typeof(DirectionComponent)
            );
            
            EntityManager.SetComponentData(entity, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(entity, new SpeedComponent { Value = 1f });
            EntityManager.SetComponentData(entity, new DirectionComponent { Value = new float3(1, 0, 0) });

            World.CreateSystem<EntityMovementSystem>();
            World.Update();
            var deltaTime = World.Time.DeltaTime;
            
            // Assert
            Assert.AreEqual(EntityManager.GetComponentData<LocalTransform>(entity).Position, new float3(deltaTime, 0, 0));
        }
        
        //the same test but with a different starting position
        [Test]
        public void PlayerMovesForwardFromPosition1()
        {
            var entity = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(SpeedComponent),
                typeof(DirectionComponent)
            );
            
            EntityManager.SetComponentData(entity, new LocalTransform { Position = new float3(0, 0, 1) });
            EntityManager.SetComponentData(entity, new SpeedComponent { Value = 1f });
            EntityManager.SetComponentData(entity, new DirectionComponent { Value = new float3(0, 0, 1) });

            World.CreateSystem<EntityMovementSystem>();
            World.Update();
            var deltaTime = World.Time.DeltaTime;
            
            // Assert
            Assert.AreEqual(EntityManager.GetComponentData<LocalTransform>(entity).Position, new float3(0, 0, 1 + deltaTime));
        }

    }
}
