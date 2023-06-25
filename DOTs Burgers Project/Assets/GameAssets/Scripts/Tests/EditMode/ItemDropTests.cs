using System.Collections;
using GameAssets.Scripts.Components;
using GameAssets.Scripts.Systems;
using GameAssets.Scripts.Temp;
using NUnit.Framework;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor;
using UnityEngine.TestTools;

namespace GameAssets.Scripts.Tests.EditMode
{
    [TestFixture]
    public class ItemDropTests : ECSTestBase
    {
        [Test]
        public void PreserveWorldPositionOnDrop()
        {
            var player = EntityManager.CreateEntity(
                typeof(PlayerInputComponent),
                typeof(ItemCarryComponent),
                typeof(LocalTransform)
                
            );

            var item = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(Parent),
                typeof(ItemComponent),
                typeof(IsPickedUpTagComponent)
            );

            var itemData = new ItemData
            {
                ItemId = 1
            };

            EntityManager.SetComponentData(player, new LocalTransform { Position = new float3(1, 1, 1) });
            EntityManager.SetComponentData(player, new ItemCarryComponent
            {
                Item = itemData,
                ItemEntity = item,
            });
            EntityManager.SetComponentData(player, new PlayerInputComponent { drop = true });

            EntityManager.SetComponentData(item, new ItemComponent { Item = itemData, });
            EntityManager.SetComponentData(item, new Parent { Value = player });
            EntityManager.SetComponentData(item, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(item, new IsPickedUpTagComponent
            {
                pickedUpBy = player,
                positionOffset = new float3(0, 1, 0)
            });

            World.CreateSystem<ItemDropSystem>();
            World.Update();

            Assert.AreEqual(new float3(1, 2, 1), EntityManager.GetComponentData<LocalTransform>(item).Position);
        }
        
        
        [Test]
        public void DropItemOnDropKey()
        {
            var player = EntityManager.CreateEntity(
                typeof(PlayerInputComponent),
                typeof(ItemCarryComponent),
                typeof(LocalTransform)
            );

            var item = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(Parent),
                typeof(ItemComponent),
                typeof(IsPickedUpTagComponent)
            );

            var itemData = new ItemData
            {
                ItemId = 1
            };

            EntityManager.SetComponentData(player, new ItemCarryComponent
            {
                Item = itemData,
                ItemEntity = item,
            });
            EntityManager.SetComponentData(player, new PlayerInputComponent { drop = true });

            EntityManager.SetComponentData(item, new ItemComponent { Item = itemData, });
            EntityManager.SetComponentData(item, new Parent { Value = player });
            EntityManager.SetComponentData(item, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(item, new IsPickedUpTagComponent
            {
                pickedUpBy = player,
                positionOffset = new float3(0, 1, 0)
            });

            World.CreateSystem<ItemDropSystem>();
            World.Update();

            Assert.IsFalse(EntityManager.HasComponent<Parent>(item));
        }

        [Test]
        public void RemovePickedUpByComponentOnDrop()
        {
            var player = EntityManager.CreateEntity(
                typeof(PlayerInputComponent),
                typeof(ItemCarryComponent),
                typeof(LocalTransform)
            );

            var item = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(Parent),
                typeof(ItemComponent),
                typeof(IsPickedUpTagComponent)
            );

            var itemData = new ItemData
            {
                ItemId = 1
            };

            EntityManager.SetComponentData(player, new ItemCarryComponent
            {
                Item = itemData,
                ItemEntity = item,
            });
            EntityManager.SetComponentData(player, new PlayerInputComponent { drop = true });

            EntityManager.SetComponentData(item, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(item, new Parent { Value = player });
            EntityManager.SetComponentData(item, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(item, new IsPickedUpTagComponent
            {
                pickedUpBy = player,
                positionOffset = new float3(0, 1, 0)
            });

            World.CreateSystem<ItemDropSystem>();
            World.Update();

            Assert.IsFalse(EntityManager.HasComponent<IsPickedUpTagComponent>(item));
        }

        [Test]
        public void AddInWorldComponentOnDrop()
        {
            var player = EntityManager.CreateEntity(
                typeof(PlayerInputComponent),
                typeof(ItemCarryComponent),
                typeof(LocalTransform)
            );

            var item = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(Parent),
                typeof(ItemComponent),
                typeof(IsPickedUpTagComponent)
            );

            var itemData = new ItemData
            {
                ItemId = 1
            };

            EntityManager.SetComponentData(player, new ItemCarryComponent
            {
                Item = itemData,
                ItemEntity = item,
            });
            EntityManager.SetComponentData(player, new PlayerInputComponent { drop = true });

            EntityManager.SetComponentData(item, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(item, new Parent { Value = player });
            EntityManager.SetComponentData(item, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(item, new IsPickedUpTagComponent
            {
                pickedUpBy = player,
                positionOffset = new float3(0, 1, 0)
            });

            World.CreateSystem<ItemDropSystem>();
            World.Update();

            Assert.IsTrue(EntityManager.HasComponent<ItemInWorldTagComponent>(item));
        }

        [Test]
        public void RemoveItemFromPlayerOnDrop()
        {
            var player = EntityManager.CreateEntity(
                typeof(PlayerInputComponent),
                typeof(ItemCarryComponent),
                typeof(LocalTransform)
            );

            var item = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(Parent),
                typeof(ItemComponent),
                typeof(IsPickedUpTagComponent)
            );

            var itemData = new ItemData
            {
                ItemId = 1
            };

            EntityManager.SetComponentData(player, new ItemCarryComponent
            {
                Item = itemData,
                ItemEntity = item,
            });
            EntityManager.SetComponentData(player, new PlayerInputComponent { drop = true });

            EntityManager.SetComponentData(item, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(item, new Parent { Value = player });
            EntityManager.SetComponentData(item, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(item, new IsPickedUpTagComponent
            {
                pickedUpBy = player,
                positionOffset = new float3(0, 1, 0)
            });

            World.CreateSystem<ItemDropSystem>();
            World.Update();

            Assert.AreEqual(ItemData.Null, EntityManager.GetComponentData<ItemCarryComponent>(player).Item);
        }

        [Test]
        public void DontDropItemsIfNoInput()
        {
            var player = EntityManager.CreateEntity(
                typeof(PlayerInputComponent),
                typeof(ItemCarryComponent),
                typeof(LocalTransform)
            );

            var item = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(Parent),
                typeof(ItemComponent),
                typeof(IsPickedUpTagComponent)
            );

            var itemData = new ItemData
            {
                ItemId = 1
            };

            EntityManager.SetComponentData(player, new ItemCarryComponent
            {
                Item = itemData,
                ItemEntity = item,
            });
            EntityManager.SetComponentData(player, new PlayerInputComponent { drop = false });

            EntityManager.SetComponentData(item, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(item, new Parent { Value = player });
            EntityManager.SetComponentData(item, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(item, new IsPickedUpTagComponent
            {
                pickedUpBy = player,
                positionOffset = new float3(0, 1, 0)
            });

            World.CreateSystem<ItemDropSystem>();
            World.Update();

            Assert.AreEqual(player, EntityManager.GetComponentData<Parent>(item).Value);
        }

        [Test]
        public void RemoveItemEntityFromCarrierOnDrop()
        {
            var player = EntityManager.CreateEntity(
                typeof(PlayerInputComponent),
                typeof(ItemCarryComponent),
                typeof(LocalTransform)
            );

            var item = EntityManager.CreateEntity(
                typeof(LocalTransform),
                typeof(Parent),
                typeof(ItemComponent),
                typeof(IsPickedUpTagComponent)
            );

            var itemData = new ItemData
            {
                ItemId = 1
            };

            EntityManager.SetComponentData(player, new ItemCarryComponent
            {
                Item = itemData ,
                ItemEntity = item
            });
            EntityManager.SetComponentData(player, new PlayerInputComponent { drop = true });

            EntityManager.SetComponentData(item, new ItemComponent { Item = itemData, });
            EntityManager.SetComponentData(item, new Parent { Value = player });
            EntityManager.SetComponentData(item, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(item, new IsPickedUpTagComponent
            {
                pickedUpBy = player,
                positionOffset = new float3(0, 1, 0)
            });

            World.CreateSystem<ItemDropSystem>();
            World.Update();

            Assert.AreEqual(Entity.Null, EntityManager.GetComponentData<ItemCarryComponent>(player).ItemEntity);
        }
    }
}