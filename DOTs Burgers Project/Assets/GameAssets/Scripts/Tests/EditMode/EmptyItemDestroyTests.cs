using GameAssets.Scripts.Components;
using GameAssets.Scripts.Systems;
using GameAssets.Scripts.Temp;
using NUnit.Framework;
using Unity.Entities;

namespace GameAssets.Scripts.Tests.EditMode
{
    [TestFixture]
    public class EmptyPickupDestroyTests : ECSTestBase
    {
        
        [Test]
        public void DestroyEmptyPickup()
        {
            var item = EntityManager.CreateEntity(
                typeof(ItemComponent)
            );
            
            EntityManager.SetComponentData(item, new ItemComponent
            {
                Item = ItemData.Null
            });

            World.CreateSystem<ItemPickupDestroySystem>();
            World.Update();
            
            //check if item has destroyed tag
            Assert.IsTrue(EntityManager.HasComponent<DestroyedTagComponent>(item));
        }

        [Test]
        public void DontDestroyIfNotEmpty()
        {
            var item = EntityManager.CreateEntity(
                typeof(ItemComponent)
            );
            
            EntityManager.SetComponentData(item, new ItemComponent
            {
                Item = new ItemData {ItemId = 1}
            });
            
            World.CreateSystem<ItemPickupDestroySystem>();
            World.Update();
            
            //check if item has destroyed tag
            Assert.IsFalse(EntityManager.HasComponent<DestroyedTagComponent>(item));
        }
    }
}