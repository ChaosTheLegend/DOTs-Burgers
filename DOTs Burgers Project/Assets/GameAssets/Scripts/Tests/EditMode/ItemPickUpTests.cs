using GameAssets.Scripts.Components;
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
        public void EntityIsSetAfterPickUp()
        {
            var player = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemCarryComponent),
                typeof(PlayerInputComponent)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemComponent),
                typeof(ItemInWorldTagComponent)
            );
            
            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = ItemData.Null });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(player, new PlayerInputComponent { pickUp = true});
            
            EntityManager.SetComponentData(itemPickup, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = float3.zero });
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            Assert.AreEqual(itemPickup, EntityManager.GetComponentData<ItemCarryComponent>(player).ItemEntity);
        }
        
        
        [Test]
        public void AddedParentAfterPickup()
        {
            var player = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemCarryComponent),
                typeof(PlayerInputComponent)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemComponent),
                typeof(ItemInWorldTagComponent)
            );
            
            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = ItemData.Null });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(player, new PlayerInputComponent { pickUp = true});
            
            EntityManager.SetComponentData(itemPickup, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = float3.zero });
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            //check if itemPickup has new component
            Assert.IsTrue(EntityManager.HasComponent<Parent>(itemPickup));
            
            //check if itemPickup has correct parent
            Assert.AreEqual(player, EntityManager.GetComponentData<Parent>(itemPickup).Value);
        }
        
        [Test]
        public void RemoveInWorldTagAfterPickup()
        {
            var player = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemCarryComponent),
                typeof(PlayerInputComponent)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemComponent),
                typeof(ItemInWorldTagComponent)
            );
            

            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = ItemData.Null });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(player, new PlayerInputComponent { pickUp = true});
            
            EntityManager.SetComponentData(itemPickup, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = float3.zero });
            
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            //check if itemPickup has new component
            Assert.IsFalse(EntityManager.HasComponent<ItemInWorldTagComponent>(itemPickup));
        }

        [Test]
        public void DontPickUpIfNoItemInWorldComponent()
        {
            var player = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemCarryComponent),
                typeof(PlayerInputComponent)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemComponent)
            );
            
            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = ItemData.Null });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(player, new PlayerInputComponent { pickUp = true});
            
            EntityManager.SetComponentData(itemPickup, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = float3.zero });
            
            Assert.AreEqual(ItemData.Null, EntityManager.GetComponentData<ItemCarryComponent>(player).Item);
        }

        [Test]
        public void PickupHasCarrierInComponent()
        {
            var player = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemCarryComponent),
                typeof(PlayerInputComponent)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemComponent),
                typeof(ItemInWorldTagComponent)
            );
            

            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = ItemData.Null });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(player, new PlayerInputComponent { pickUp = true});
            
            EntityManager.SetComponentData(itemPickup, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = float3.zero });
            
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            //check if itemPickup has new component
            Assert.AreEqual(player ,EntityManager.GetComponentData<IsPickedUpTagComponent>(itemPickup).pickedUpBy);
        }
        
        
        [Test]
        public void AddPickUpTagAfterPickup()
        {
            var player = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemCarryComponent),
                typeof(PlayerInputComponent)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemComponent),
                typeof(ItemInWorldTagComponent)
            );
            

            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = ItemData.Null });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(player, new PlayerInputComponent { pickUp = true});
            
            EntityManager.SetComponentData(itemPickup, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = float3.zero });
            
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            //check if itemPickup has new component
            Assert.IsTrue(EntityManager.HasComponent<IsPickedUpTagComponent>(itemPickup));
        }
        
        
        [Test]
        public void PickUpItemIfNear()
        {
            
            var player = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemCarryComponent),
                typeof(PlayerInputComponent)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemComponent),
                typeof(ItemInWorldTagComponent)
            );
            

            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = ItemData.Null });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(player, new PlayerInputComponent { pickUp = true});
            
            EntityManager.SetComponentData(itemPickup, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = float3.zero });
            
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            
            Assert.AreEqual(itemData.ItemId,EntityManager.GetComponentData<ItemCarryComponent>(player).Item.ItemId);
        }

        [Test]
        public void DontPickUpItemIfFarAway()
        {
            
            var player = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemCarryComponent),
                typeof(PlayerInputComponent)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemComponent),
                typeof(ItemInWorldTagComponent)
            );
            

            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = ItemData.Null });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(player, new PlayerInputComponent { pickUp = true});
            
            EntityManager.SetComponentData(itemPickup, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = new float3(100,100,100) });
            
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            
            Assert.AreEqual(ItemData.Null,EntityManager.GetComponentData<ItemCarryComponent>(player).Item);
        }


        [Test]
        public void DontPickUpItemIfNotEmpty()
        {
            
            var player = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemCarryComponent),
                typeof(PlayerInputComponent)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemComponent),
                typeof(ItemInWorldTagComponent)
            );
            

            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            var itemData2 = new ItemData()
            {
                ItemId = 2
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = itemData });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(player, new PlayerInputComponent { pickUp = true});
            
            EntityManager.SetComponentData(itemPickup, new ItemComponent { Item = itemData2 });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = float3.zero });
            
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            
            Assert.AreEqual(itemData.ItemId,EntityManager.GetComponentData<ItemCarryComponent>(player).Item.ItemId);
        }
        
        [Test]
        public void DontPickUpDataIfKeyNotPressed()
        {
            
            var player = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemCarryComponent),
                typeof(PlayerInputComponent)
            );
            
            var itemPickup = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(ItemComponent),
                typeof(ItemInWorldTagComponent)
            );
            

            var itemData = new ItemData()
            {
                ItemId = 1
            };
            
            EntityManager.SetComponentData(player, new ItemCarryComponent { Item = ItemData.Null });
            EntityManager.SetComponentData(player, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(player, new PlayerInputComponent { pickUp = false});
            
            EntityManager.SetComponentData(itemPickup, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(itemPickup, new LocalTransform { Position = float3.zero });
            
            
            World.CreateSystem<ItemPickupSystem>();
            World.Update();
            
            
            Assert.AreEqual(ItemData.Null,EntityManager.GetComponentData<ItemCarryComponent>(player).Item);
        }
    }
}