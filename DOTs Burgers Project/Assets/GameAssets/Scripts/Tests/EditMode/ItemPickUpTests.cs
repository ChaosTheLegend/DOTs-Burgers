using GameAssets.Scripts.Temp;
using NUnit.Framework;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GameAssets.Scripts.Tests.EditMode
{
    [TestFixture]
    public class ItemPickUpTests : ECSTestBase
    {
        [Test]
        public void PickUpItemIfNear()
        {
            
            var player = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemCarryComponent)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemPickupComponent)
            );
            

            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = ItemData.Null });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });

            EntityManager.SetComponentData(itemPickup, new ItemPickupComponent { Item = itemData });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = float3.zero });
            
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            
            Assert.AreEqual(itemData.ItemId,EntityManager.GetComponentData<ItemCarryComponent>(player).Item.ItemId);
        }

        [Test]
        public void DontPickUpItemIfFarAway()
        {
            var player = EntityManager.CreateEntity(
                typeof(ItemCarryComponent), 
                typeof(LocalTransform)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(ItemPickupComponent),
                typeof(LocalTransform)
            );
            
            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = ItemData.Null });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });

            EntityManager.SetComponentData(itemPickup, new ItemPickupComponent { Item = itemData });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = new float3(0,0,10) });
            
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            Assert.AreEqual(ItemData.Null.ItemId,EntityManager.GetComponentData<ItemCarryComponent>(player).Item.ItemId);
        }
        
        [Test]
        public void DontPickUpItemIfAlreadyCarrying()
        {
            var player = EntityManager.CreateEntity(
                typeof(ItemCarryComponent), 
                typeof(LocalTransform)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(ItemPickupComponent),
                typeof(LocalTransform)
            );
            
            var itemData1 = new ItemData()
            {
                ItemId = 1
            };
            
            var itemData2 = new ItemData()
            {
                ItemId = 2
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = itemData1 });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });

            EntityManager.SetComponentData(itemPickup, new ItemPickupComponent { Item = itemData2 });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = float3.zero });
            
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            Assert.AreEqual(itemData1.ItemId,EntityManager.GetComponentData<ItemCarryComponent>(player).Item.ItemId);
        }
    }
}