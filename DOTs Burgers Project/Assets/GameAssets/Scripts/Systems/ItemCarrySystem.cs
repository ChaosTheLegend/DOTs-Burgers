using GameAssets.Scripts.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace GameAssets.Scripts.Systems
{
    public partial struct ItemCarrySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var item in SystemAPI.Query<CarriedItemAspect>())
            {
                item.ResetParentIfIncorrect();
                item.GlueToParent();
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}