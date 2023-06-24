using GameAssets.Scripts.Components;
using GameAssets.Scripts.Systems;
using GameAssets.Scripts.Temp;
using NUnit.Framework;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GameAssets.Scripts.Tests.EditMode
{
    [TestFixture]
    public class ItemCarryTests : ECSTestBase
    {
        [Test]
        public void IsCarryingOverHead()
        {
            var carrier = EntityManager.CreateEntity(
                typeof(ItemCarryComponent)
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
            
            EntityManager.SetComponentData(carrier, new ItemCarryComponent
            {
                Item = itemData
            });
            
            EntityManager.SetComponentData(item, new ItemComponent { Item = itemData });
            EntityManager.SetComponentData(item, new Parent { Value = carrier });
            EntityManager.SetComponentData(item, new LocalTransform { Position = float3.zero });
            EntityManager.SetComponentData(item, new IsPickedUpTagComponent
            {
                pickedUpBy = carrier,
                positionOffset = new float3(0, 1, 0)
            });

            World.CreateSystem<ItemCarrySystem>();
            World.Update();
            
            Assert.AreEqual(new float3(0, 1, 0), EntityManager.GetComponentData<LocalTransform>(item).Position);
        }
    }
}