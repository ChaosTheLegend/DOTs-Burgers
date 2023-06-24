using GameAssets.Scripts.Components;
using GameAssets.Scripts.Systems;
using GameAssets.Scripts.Temp;
using NUnit.Framework;
using Unity.Entities;

namespace GameAssets.Scripts.Tests.EditMode
{
    [TestFixture]
    public class EntityDestroyTests : ECSTestBase
    {
        
        [Test]
        public void DestroyEntityIfHasTag()
        {
            var item = EntityManager.CreateEntity(
                typeof(DestroyedTagComponent)
            );
            
            World.CreateSystem<EntityDestroySystem>();
            World.Update();
            World.Update();
            
            Assert.IsFalse(EntityManager.Exists(item));
        }
        
        [Test]
        public void DontDestroyIfNoTag()
        {
            var item = EntityManager.CreateEntity(
                
            );
            
            
            World.CreateSystem<EntityDestroySystem>();
            World.Update();
            
            Assert.IsTrue(EntityManager.Exists(item));
        }
        
    }
}