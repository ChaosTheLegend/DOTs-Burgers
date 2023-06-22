using NUnit.Framework;
using Unity.Entities;

namespace GameAssets.Scripts.Tests.PlayMode
{
    [TestFixture]
    public class MovementTests
    {
        [SetUp]
        private void Setup()
        {
            // var entityManager = new World("TestWorld").EntityManager;
            // var entity = entityManager.CreateEntity(
            //     typeof(Translation),
            //     typeof(PlayerSpeed),
            //     typeof(PlayerDirection)
            // );
            // entityManager.SetComponentData(entity, new Translation { Value = float3.zero });
            // entityManager.SetComponentData(entity, new PlayerSpeed { Value = 5f });
            // entityManager.SetComponentData(entity, new PlayerDirection { Value = Direction.Forward });
        }
        
        [Test]
        public void PlayerMovesForward()
        {
        //     // Arrange
        //     var entityManager = new World("TestWorld").EntityManager;
        //     var entity = entityManager.CreateEntity(
        //         typeof(Translation),
        //         typeof(PlayerSpeed),
        //         typeof(PlayerDirection)
        //     );
        //     entityManager.SetComponentData(entity, new Translation { Value = float3.zero });
        //     entityManager.SetComponentData(entity, new PlayerSpeed { Value = 5f });
        //     entityManager.SetComponentData(entity, new PlayerDirection { Value = Direction.Forward });
        //
        //     // Act
        //     new MovementSystem().Update();
        //
        //     // Assert
        //     Assert.AreEqual(entityManager.GetComponentData<Translation>(entity).Value, Vector3.forward * 5f, 0.001f);
        }

    }
}
