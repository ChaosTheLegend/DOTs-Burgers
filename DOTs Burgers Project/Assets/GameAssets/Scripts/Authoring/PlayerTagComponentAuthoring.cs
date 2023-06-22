using GameAssets.Scripts.Aspects;
using Rewired;
using Unity.Entities;
using UnityEngine;

namespace GameAssets.Scripts.Authoring
{
    public class PlayerTagComponentAuthoring : MonoBehaviour
    {
        public int AssignedPlayerId;

        public class PlayerTagComponentBaker : Baker<PlayerTagComponentAuthoring>
        {
            public override void Bake(PlayerTagComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerTagComponent { AssignedPlayerId = authoring.AssignedPlayerId });
            }
        }
    }
}