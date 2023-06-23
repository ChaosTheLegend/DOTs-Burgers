using GameAssets.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace GameAssets.Scripts.Authoring
{
    public class DirectionComponentAuthoring : MonoBehaviour
    {
        [SerializeField]
        public float3 direction;

        public class DirectionComponentBaker : Baker<DirectionComponentAuthoring>
        {
            public override void Bake(DirectionComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new DirectionComponent { Value = authoring.direction });
            }
        }
    }
}