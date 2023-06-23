using GameAssets.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace GameAssets.Scripts.Authoring
{
    public class SpeedComponentAuthoring : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        public class SpeedComponentBaker : Baker<SpeedComponentAuthoring>
        {
            public override void Bake(SpeedComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new SpeedComponent { Value = authoring.speed });
            }
        }
    }
}