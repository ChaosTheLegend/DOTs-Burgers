using GameAssets.Scripts.Aspects;
using GameAssets.Scripts.Components;
using GameAssets.Scripts.Temp;
using Unity.Burst;
using Unity.Entities;

namespace GameAssets.Scripts.Systems
{
    public partial struct EntityDestroySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.EntityManager.DestroyEntity(state.EntityManager.CreateEntityQuery(typeof(DestroyedTagComponent)));
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}